using Eleks.Yurii.Fozekosh.CoreQuizz.BAL.Contracts;
using Eleks.Yurii.Fozekosh.CoreQuizz.WebService.ViewModel.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.WebService.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountManager _manager;

        public AccountController(ILogger<AccountController> logger, IAccountManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginInfo)
        {
            _logger.LogInformation($"Login info: {loginInfo.Login} ");
            if (_manager.IsUserExists(loginInfo.Login))
            {
                _logger.LogInformation($"Login {loginInfo.Login} not exists in db");
                _manager.RegisterUser(loginInfo.Login, loginInfo.Password);
            }

            _logger.LogInformation($"Login {loginInfo.Login} exists in db");
            HttpContext.Session.SetString("login", loginInfo.Login);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
