using Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.WebService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
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