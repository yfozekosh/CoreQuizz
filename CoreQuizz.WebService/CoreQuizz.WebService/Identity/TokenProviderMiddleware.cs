using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreQuizz.BAL.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CoreQuizz.WebService.Identity
{
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UserManager<AuthenticationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAccountManager _accountManager;
        private readonly TokenProviderOptions _options;

        public TokenProviderMiddleware(
            RequestDelegate next,
            IOptions<TokenProviderOptions> options,
            UserManager<AuthenticationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _next = next;
            _userManager = userManager;
            _roleManager = roleManager;
            _options = options.Value;
        }

        public Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
            {
                return _next(context);
            }

            if (!context.Request.Method.Equals("POST")
                || context.Request.ContentType != "application/json")
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync(SeriaizeErrorResponse("Only application/json"));
            }


            string json = new StreamReader(context.Request.Body).ReadToEnd();
            TokenModel data = JsonConvert.DeserializeObject<TokenModel>(json);
            string username = data.Username;
            string password = data.Password;

            return GenerateToken(context, username, password);
        }

        private async Task GenerateToken(HttpContext context, string username, string password)
        {
            AuthenticationUser user = await _userManager.FindByEmailAsync(username);
            ClaimsIdentity identity = await GetIdentity(user, username, password);

            if (identity == null)
            {
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync(
                    SeriaizeErrorResponse("Invalid username or password."));

                return;
            }

            identity.AddClaims(await GetUserClaims(username, user));

            DateTime now = DateTime.UtcNow;
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            JwtSecurityToken jwt = handler.CreateJwtSecurityToken(subject: identity,
                signingCredentials: _options.SigningCredentials,
                issuer: _options.Issuer,
                audience: _options.Audience,
                notBefore: now,
                expires: now.Add(_options.Expiration));

            string encodedJwt = handler.WriteToken(jwt);

            var response =new TokenResponce()
            {
                AccessToken = encodedJwt,
                ExpiresIn = (int)_options.Expiration.TotalSeconds
            };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(SeriaizeOkResponse(response));
        }

        private async Task<ClaimsIdentity> GetIdentity(AuthenticationUser user, string username, string password)
        {
            if (user == null) return null;

            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

            if (isPasswordValid)
            {
                return new ClaimsIdentity(new System.Security.Principal.GenericIdentity(username, "Token"), new Claim[] { });
            }

            // Credentials are invalid, or account doesn't exist
            return null;
        }

        private async Task<List<Claim>> GetUserClaims(string username, AuthenticationUser user)
        {
            var nowSeconds = TimeSpan.FromTicks(DateTime.UtcNow.Ticks);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, nowSeconds.ToString(), ClaimValueTypes.Integer64),
                new Claim(CustomClaimType.UserId, user.CoreQuizzUserId.ToString())
            };

            IList<string> roleStrs = await _userManager.GetRolesAsync(user);

            IEnumerable<Claim> rolesAsClaims = roleStrs.Select(role => new Claim(ClaimTypes.Role, role));

            IdentityRole[] roles = await Task.WhenAll(roleStrs.Select(r => _roleManager.FindByNameAsync(r)));

            IEnumerable<Claim> roleClaims =
                roles.SelectMany(r => r.Claims).Select(x => new Claim(x.ClaimType, x.ClaimValue));

            claims.AddRange(rolesAsClaims);
            claims.AddRange(roleClaims);
            return claims;
        }

        private string SeriaizeOkResponse(TokenResponce response)
        {
            return ServiceObjectResponse(new OkServiceResponse<TokenResponce>(response));
        }

        private string SeriaizeErrorResponse(string response)
        {
            return ServiceObjectResponse(new ErrorServiceRespose(response));
        }

        private string ServiceObjectResponse(object response)
        {
            return JsonConvert.SerializeObject(response,
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }
    }
}