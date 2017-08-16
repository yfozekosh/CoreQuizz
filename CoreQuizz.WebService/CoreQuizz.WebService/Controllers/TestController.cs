using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreQuizz.DataAccess.Contract.Contracts;
using CoreQuizz.Queries.Contracts;
using CoreQuizz.Queries.PageQueries;
using Microsoft.AspNetCore.Mvc;

namespace CoreQuizz.WebService.Controllers
{
    public class TestController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public TestController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public IActionResult Index()
        {
            SurveyListItem[] result = _queryDispatcher.Execute<SurveyListPageQuery, SurveyListItem[]>(new SurveyListPageQuery()
            {
                UserId = 5
            });

            return View(result);
        }
    }
}