using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace CoreQuizz.WebService.Controllers
{
    public class UserViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; } = false;
    }

    [Route("/api/access")]
    public class AccessController : Controller
    {
        [Route("register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody]UserViewModel model)
        {
            string token = await HttpContext.Authentication.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
            Debug.WriteLine($"Token: {token}");
            foreach (var userClaim in User.Claims)
            {
                Debug.WriteLine($"Claim type: {userClaim.Type} ; value:{userClaim.Value}");
            }
            return Ok(token);

        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login([FromBody] UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return BadRequest();
        }

        [Route("api-test")]
        [HttpGet]
        [Authorize]
        public ActionResult AccessTest()
        {
            return Ok("passed");
        }
    }
}
