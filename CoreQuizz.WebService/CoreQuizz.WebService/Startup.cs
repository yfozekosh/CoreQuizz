using System;
using CoreQuizz.BAL.Contracts;
using CoreQuizz.BAL.Managers.Extensions;
using CoreQuizz.DataAccess.Extensions;
using CoreQuizz.Queries.Extensions;
using CoreQuizz.Shared;
using CoreQuizz.WebService.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

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

            services.AddIdentity<AuthenticationUser, IdentityRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 8;
                })
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

            app.UserCoreQuizzJwt(serviceProvider);

            app.UseSession();
            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();

            if (env.IsDevelopment())
            {
                IAccountManager accountManager = serviceProvider.GetService<IAccountManager>();
                ISurveyManager surveyManager = serviceProvider.GetService<ISurveyManager>();
                UserManager<AuthenticationUser> userManager =
                    serviceProvider.GetService<UserManager<AuthenticationUser>>();
                RoleManager<IdentityRole> roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
                var role = roleManager.FindByNameAsync("admin").GetAwaiter().GetResult();
                if (role == null)
                {
                    role = new IdentityRole("admin");
                    var roleResult = roleManager.CreateAsync(role).GetAwaiter().GetResult();
                    if (!roleResult.Succeeded)
                        throw new NotImplementedException(string.Join(",", roleResult.Errors));
                }

                var user = userManager.FindByEmailAsync("yfozekosh@gmail.com").GetAwaiter().GetResult();
                if (user == null)
                {
                    user = new AuthenticationUser()
                    {
                        Email = "yfozekosh@gmail.com",
                        UserName = "yfozekosh@gmail.com",
                        EmailConfirmed = true
                    };
                    var adminRole = roleManager.FindByNameAsync("admin").GetAwaiter().GetResult();
                    IdentityResult createResult = userManager.CreateAsync(user, "H25mc1bb").GetAwaiter().GetResult();
                    if (!createResult.Succeeded)
                    {
                        throw new NotImplementedException(string.Join("\r\n", createResult.Errors));
                    }
                    var use = userManager.FindByEmailAsync("yfozekosh@gmail.com").GetAwaiter().GetResult();
                    var res = userManager.AddToRoleAsync(use, "admin").GetAwaiter().GetResult();
                    if (!res.Succeeded)
                        throw new NotImplementedException(string.Join("\r\n", res.Errors));
                }
                string email = "yfozekosh@gmail.com";

                BAL.AppSeed.SeedDatabaseIfDevelop(accountManager, surveyManager, email);
            }
        }
    }
}