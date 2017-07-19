using System;
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
            bool isLoggedIn;
            try
            {
                isLoggedIn = _manager.LogInUser(loginInfo.Login, loginInfo.Password);
            }
            catch (ArgumentException e)
            {
                _logger.LogInformation(e.Message);
                isLoggedIn = false;
            }

            if (!isLoggedIn)
            {
                return View(new LoginViewModel
                {
                    IsIncorrect = true
                });
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
