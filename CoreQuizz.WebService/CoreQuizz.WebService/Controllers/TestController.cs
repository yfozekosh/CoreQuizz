using CoreQuizz.Queries.Contract;
using CoreQuizz.Queries.PageQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreQuizz.WebService.Controllers
{
    [Authorize]
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