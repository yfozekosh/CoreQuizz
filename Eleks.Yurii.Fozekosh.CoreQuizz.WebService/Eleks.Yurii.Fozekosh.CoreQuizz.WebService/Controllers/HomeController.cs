using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.WebService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET
        public ActionResult Index()
        {
            _logger.LogInformation("accessing home");
            var user = HttpContext.Session.GetString("login");
            if (user != null)
            {
                return View((object)user);
            }
            return RedirectToAction("Login", "Account");
        }
    }
}