using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreQuizz.WebService.Identity
{
    public static class AuthenticationConfigure
    {
        public static void UserCoreQuizzJwt(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            string secretKey = "CoreQuizz_key_9352_{wq[[SS]]ds,,.!!";
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var options = new TokenProviderOptions
            {
                Audience = "https://therium-labs.com",
                Issuer = "corequizz",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
            };

            var tokenValidationParameters = new TokenValidationParameters
            {                
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                
                ValidateIssuer = true,
                ValidIssuer = "corequizz",

                ValidateAudience = true,
                ValidAudience = "https://therium-labs.com",

                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options),
                serviceProvider.GetService<UserManager<AuthenticationUser>>());

            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                Audience = "https://therium-labs.com",
                AutomaticAuthenticate = true,
                TokenValidationParameters = tokenValidationParameters
            });

        }
    }
}