using System;
using System.Collections.Generic;
using System.Linq;
using CoreQuizz.DataAccess.Contracts;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Shared.DomainModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CoreQuizz.WebService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SurveyContext _surveyContext;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, SurveyContext surveyContext)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _surveyContext = surveyContext;
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
                .Get(x => x.CreatedBy.Email == "yfozekosh@gmail.com", survey => survey.CreatedBy,
                    survey => survey.Questions).ToList();

            var opts = _unitOfWork.GetRepository<QuestionOption>();

            surveys.ForEach(x =>
            {

                foreach (var objQuestion in x.Questions)
                {
                    RadioQuestion radio = objQuestion as RadioQuestion;
                    CheckboxQuestion checkbox = objQuestion as CheckboxQuestion;
                    InputQuestion input = objQuestion as InputQuestion;
                    if (radio != null)
                    {
                        radio.Options = opts.Get(y => y.Question != null && y.Question.Id==radio.Id, option => option.Question).ToList();
                        _logger.LogInformation($"radio got. Options count = {radio.Options.Count}");
                    }
                    if (checkbox != null)
                    {
                        checkbox.Options =
                            opts.Get(y => y.Question != null && y.Question.Id == checkbox.Id, option => option.Question)
                                .ToList();
                        _logger.LogInformation($"checbox got. Options count = {checkbox.Options.Count}");
                    }
                    if (input != null)
                    {
                        input.Value += "_Approved";
                        _logger.LogInformation("input got. Approved");
                    }
                }
            });
            return Json(surveys, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}