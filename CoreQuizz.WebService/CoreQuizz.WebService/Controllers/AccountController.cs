using System;
using System.Linq;
using System.Threading.Tasks;
using CoreQuizz.BAL.Contracts;
using CoreQuizz.BAL.Managers;
using CoreQuizz.Shared.DomainModel;
using CoreQuizz.WebService.Identity;
using CoreQuizz.WebService.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreQuizz.WebService.Controllers
{
    [Route("/api/account")]
    public class AccountController : Controller
    {
        private readonly UserManager<AuthenticationUser> _userManager;
        private readonly IAccountManager _accountManager;

        public AccountController(UserManager<AuthenticationUser> userManager, IAccountManager accountManager)
        {
            _userManager = userManager;
            _accountManager = accountManager;
        }

        [Route("register")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ServiceResponse> Register([FromBody]RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid && registerViewModel.Password == registerViewModel.Password2)
            {
                var user = new AuthenticationUser()
                {
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.Email,
                    EmailConfirmed = true
                };

                IdentityResult result = await _userManager.CreateAsync(user, registerViewModel.Password);
                if (!result.Succeeded)
                {
                    return new ErrorServiceRespose(String.Join("\r\n", result.Errors.Select(e => e.Description)));
                }

                OperationResult<User> registerResult = await _accountManager.RegisterUserAsync(registerViewModel.Email);

                if (!registerResult.IsSuccess)
                {
                    return new ErrorServiceRespose(String.Join("\r\n", registerResult.Exceptions.Select(e=>e.Message)));
                }

                User quizzUser = registerResult.Result;
                user.CoreQuizzUserId = quizzUser.Id;

                await _userManager.UpdateAsync(user);

                return new OkServiceResponse<string>("created");
            }

            return new ErrorServiceRespose("register data is not valid");
        }
    }
}