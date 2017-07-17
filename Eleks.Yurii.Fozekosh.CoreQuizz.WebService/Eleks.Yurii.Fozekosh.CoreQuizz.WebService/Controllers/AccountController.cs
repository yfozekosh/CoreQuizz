using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eleks.Yurii.Fozekosh.CoreQuizz.WebService.ViewModel.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.WebService.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(LoginViewModel loginInfo)
        {
            _logger.LogInformation($"Login info: {loginInfo.Login} / {loginInfo.Password}");
            HttpContext.Session.SetString("login",loginInfo.Login);
            return RedirectToAction("Index", "Home");
        }
    }
}
