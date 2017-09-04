using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CoreQuizz.BAL.Contracts;
using CoreQuizz.BAL.Managers.Extensions;
using CoreQuizz.DataAccess.Extensions;
using CoreQuizz.Queries.Extensions;
using CoreQuizz.Shared;
using CoreQuizz.WebService.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using NLog.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace CoreQuizz.WebService
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            env.ConfigureNLog("nlog.config");
            var builder = new ConfigurationBuilder()
                           .SetBasePath(env.ContentRootPath)
                           .AddJsonFile("appconfig.json", optional: true, reloadOnChange: true);


            Configuration = builder.Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppConfig>(Configuration);

            services.AddDAL(Configuration);

            services.AddTransient<IDependencyResolver, AspNetCoreDependencyResolver>();

            services.AddBAL();
            services.AddQueries();

            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(Configuration["survey_connection"]));

            services.AddIdentity<AuthenticationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>();

            services.AddMvc();

            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {

            loggerFactory.AddConsole();
            loggerFactory.AddNLog();

            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();
            //}

            string secretKey = "CoreQuizz_key_9352_{wq[[SS]]ds,,.!!";
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var options = new TokenProviderOptions
            {
                Audience = "ExampleAudience",
                Issuer = "ExampleIssuer",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
            };

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = "ExampleIssuer",

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = "ExampleAudience",

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };
            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));

            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                Audience = "https://localhost:44304/",
                AutomaticAuthenticate = true,
                TokenValidationParameters = tokenValidationParameters
            });

            app.UseSession();
            app.UseStaticFiles();

            //app.UseIdentityServer(Configuration);
            //app.UseIdentity();
            app.UseMvcWithDefaultRoute();

            if (env.IsDevelopment())
            {
                IAccountManager accountManager = serviceProvider.GetService<IAccountManager>();
                ISurveyManager surveyManager = serviceProvider.GetService<ISurveyManager>();
                string email = "yfozekosh@gmail.com";

                BAL.AppSeed.SeedDatabaseIfDevelop(accountManager, surveyManager, email);
            }
        }
    }
}