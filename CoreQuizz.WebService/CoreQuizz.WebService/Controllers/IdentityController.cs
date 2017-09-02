using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreQuizz.Shared.DomainModel;
using CoreQuizz.WebService.Identity;
using CoreQuizz.WebService.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace CoreQuizz.WebService.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<AuthenticationUser> _userManager;
        private readonly SignInManager<AuthenticationUser> _signInManager;

        public IdentityController(UserManager<AuthenticationUser> userManager, SignInManager<AuthenticationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Register");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (model.Password != model.Password2)
            {
                ModelState.AddModelError("example", "password different");
            }

            if (ModelState.IsValid)
            {
                var user = new AuthenticationUser { Email = model.Email, UserName = model.Email };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Res");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                AuthenticationUser user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return View();
                }

                SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (signInResult.Succeeded)
                {
                    RedirectToAction("Res");
                }
                return Json(model);
            }
            return View();
        }

        public IActionResult Res()
        {
            return View();
        }
    }
}