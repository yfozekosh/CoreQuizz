using System.Collections.Generic;
using System.Linq;
using CoreQuizz.BAL.Contracts;
using CoreQuizz.DataAccess.Contract.Contracts;
using CoreQuizz.Shared.DomainModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreQuizz.WebService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISurveyManager _surveyManager;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, ISurveyManager surveyManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _surveyManager = surveyManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            _logger.LogInformation("accessing home");
            var login = HttpContext.Session.GetString("login");
            if (login != null)
            {
                var user = _unitOfWork.GetRepository<User>().Get(x => x.Email == login).FirstOrDefault();
                if (user != null)
                {
                    var s = new Survey
                    {
                        Title = "Title",
                        Questions = new List<Question>
                        {
                            new CheckboxQuestion
                            {
                                QuestionLabel = "Checkbox",
                                Options = new List<QuestionOption>
                                {
                                    new QuestionOption {IsSelected = false, Value = "val1"},
                                    new QuestionOption {IsSelected = false, Value = "val2"},
                                    new QuestionOption {IsSelected = false, Value = "val3"}
                                }
                            },
                            new RadioQuestion
                            {
                                QuestionLabel = "radio",
                                Options = new List<QuestionOption>
                                {
                                    new QuestionOption {Value = "val1"},
                                    new QuestionOption {Value = "val1"},
                                    new QuestionOption {Value = "val1"}
                                }
                            },
                            new InputQuestion
                            {
                                QuestionLabel = "lol"
                            }
                        }
                    };
                    _unitOfWork.GetRepository<Survey>().Add(s);
                    if (user.Surveys == null)
                        user.Surveys = new List<Survey>();
                    user.Surveys.Add(s);
                    _unitOfWork.GetRepository<User>().Update(user);
                    _unitOfWork.Save();
                }
                return View((object)login);
            }
            return RedirectToAction("Login", "Account");
        }

        public IActionResult DbTest()
        {
            var surveys = _unitOfWork.GetRepository<Survey>()
                .Get(x => x.CreatedBy.Email == "yfozekosh@gmail.com", survey => survey.CreatedBy).ToList();

            _surveyManager.LoadFullSurveys(surveys);

            return Json(surveys, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}