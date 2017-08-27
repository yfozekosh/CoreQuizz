using Microsoft.AspNetCore.Mvc;

namespace CoreQuizz.WebService.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return Redirect("http://localhost:4200");
        }
    }
}