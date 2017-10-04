using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
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
using CoreQuizz.Commands.Extensions;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.WebService.Communication.ModelBinders;
using CoreQuizz.WebService.Identity.JWT;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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

            services.AddCommands();
            services.AddQueries();

            if (Configuration["db"] == "mssql")
            {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(Configuration["survey_connection"]));
            }
            else if (Configuration["db"] == "sqlite")
            {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlite(Configuration["sqlite_connection"]));
            }

            services.AddIdentity<AuthenticationUser, IdentityRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 8;
                })
                .AddEntityFrameworkStores<IdentityContext>();

            services.AddMvc(options =>
            {
                options.OutputFormatters.Add(new JsonOutputFormatter(new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = new List<JsonConverter>()
                    {
                        new QuestionJsonConverter()
                    }
                }, ArrayPool<char>.Shared));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            IServiceProvider serviceProvider)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UserCoreQuizzJwt(serviceProvider);
            
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.Use(async (context, func) =>
                {
                    context.Request.EnableRewind();
                    var sr = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024 * 100, true);
                    Console.WriteLine(await sr.ReadToEndAsync());
                    context.Request.Body.Position = 0;
                    await func.Invoke();
                });
            }

            app.UseMvc(builder =>
            {
                builder.MapRoute("default", "{controller}/{action}", new
                {
                    Controller = "Home",
                    Action = "Index"
                });
                builder.MapRoute("api", "api/{controller}/{action}");
            });

            SeedDbIfDevelop(env, serviceProvider);
        }

        private void SeedDbIfDevelop(IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                var surveyc = serviceProvider.GetService<SurveyContext>();

                surveyc.Database.Migrate();


                var identc = serviceProvider.GetService<IdentityContext>();

                identc.Database.Migrate();


                IAccountManager accountManager = serviceProvider.GetService<IAccountManager>();

                UserManager<AuthenticationUser> userManager =
                    serviceProvider.GetService<UserManager<AuthenticationUser>>();
                RoleManager<IdentityRole> roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
                var role = roleManager.FindByNameAsync("admin").GetAwaiter().GetResult();
                if (role == null)
                {
                    role = new IdentityRole("admin")
                    {
                        Claims =
                        {
                            AccessClaim(Accessess.QuestionCreation),
                            AccessClaim(Accessess.SurveyCreation),
                            AccessClaim(Accessess.SurveyStatistic)
                        }
                    };
                    var roleResult = roleManager.CreateAsync(role).GetAwaiter().GetResult();
                    if (!roleResult.Succeeded)
                        throw new NotImplementedException(string.Join(",", roleResult.Errors));

                    role = new IdentityRole("user")
                    {
                        Claims =
                        {
                            AccessClaim(Accessess.SurveyCreationPerUser),
                            AccessClaim(Accessess.QuestionCreationPerUser),
                            AccessClaim(Accessess.QuestionCreationPerSurvey),
                            AccessClaim(Accessess.SurveyStatisticPerUser),
                            AccessClaim(Accessess.SurveyStatisticPerSurvey)
                        }
                    };
                    roleResult = roleManager.CreateAsync(role).GetAwaiter().GetResult();
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

                    var quizzUser = accountManager.RegisterUserAsync("yfozekosh@gmail.com").GetAwaiter().GetResult()
                        .Result;
                    user.CoreQuizzUserId = quizzUser.Id;
                    userManager.UpdateAsync(user).GetAwaiter().GetResult();
                }
            }
        }

        private IdentityRoleClaim<string> AccessClaim(string access)
        {
            return new IdentityRoleClaim<string>()
            {
                ClaimType = CustomClaimType.Access,
                ClaimValue = access
            };
        }
    }
}