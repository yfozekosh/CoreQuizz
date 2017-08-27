using CoreQuizz.BAL.Contracts;
using CoreQuizz.Shared.DomainModel;
using CoreQuizz.WebService.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using CoreQuizz.BAL.Contracts.QuestionChain;
using Newtonsoft.Json;

namespace CoreQuizz.WebService.Controllers
{
    [Route("api/survey")]
    public class SurveyController : Controller
    {
        private readonly ISurveyManager _surveyManager;
        private readonly ILogger<SurveyController> _logger;
        private readonly ISessionManagerFactory _sessionManagerFactory;
        private readonly IQuestionChainFactory _chainFactory;

        public SurveyController(ISurveyManager surveyManager,
                                IQuestionManager questionManager,
                                ILogger<SurveyController> logger,
                                ISessionManagerFactory sessionManagerFactory,
                                IQuestionChainFactory chainFactory)
        {
            _surveyManager = surveyManager;
            _logger = logger;
            _sessionManagerFactory = sessionManagerFactory;
            _chainFactory = chainFactory;
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            return Json(_surveyManager.LoadFullSurveys(_surveyManager.GetSurveys()), new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        [HttpGet("{id}", Name = "GetSurvey")]
        public Survey Get(int id)
        {
            return _surveyManager.GetSurvey(id);
        }

        [HttpPost]
        public ActionResult SaveSurvey(Survey survey)
        {
            if (survey == null) return BadRequest("Survey cannot be null");
            if (survey.Questions == null) return BadRequest("Questions is undefined");


            try
            {
                bool isQuestionFilled = _chainFactory.GetQuestionChecker().IsQuestionsFilled(survey.Questions);
                if (!isQuestionFilled) return BadRequest("Questions without options are not allowed");

                string login = _sessionManagerFactory.GetSessionManager(HttpContext.Session).CurrentLogin;
                _surveyManager.CreateSurvey(survey, login);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e.StackTrace);
                throw;
            }
        }

        [HttpDelete]
        public ActionResult DeleteSurvey(int id)
        {
            if (id <= 0) return BadRequest($"Id out of range. id={0}");

            try
            {
                string login = _sessionManagerFactory.GetSessionManager(HttpContext.Session).CurrentLogin;
                Survey surveyToDelete = _surveyManager.GetSurveys(survey => survey.CreatedBy.Email == login && survey.Id == id).FirstOrDefault();
                if (surveyToDelete != null)
                    _surveyManager.DeleteSurvey(surveyToDelete);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e.StackTrace);
                throw;
            }
        }
    }
}