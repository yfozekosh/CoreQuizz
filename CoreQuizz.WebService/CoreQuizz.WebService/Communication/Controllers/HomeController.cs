using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CoreQuizz.WebService.Communication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly IOptions<AppConfig> _configuration;

        public HomeController(IHostingEnvironment env, IOptions<AppConfig> configuration)
        {
            _env = env;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            if (_env.IsDevelopment())
            {
                return Redirect(_configuration.Value.FrontendHostingUrl);
            }

            return Redirect("/index.html");            
        }
    }
}