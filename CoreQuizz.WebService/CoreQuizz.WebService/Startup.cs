using System;
using System.Collections.Generic;
using CoreQuizz.BAL;
using CoreQuizz.BAL.Contracts;
using CoreQuizz.BAL.Extensions;
using CoreQuizz.DataAccess.Contract.Contracts;
using CoreQuizz.DataAccess.DAL;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Shared.DomainModel;
using CoreQuizz.WebService.Session;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddDbContext<SurveyContext>(options => options.UseSqlServer(Configuration["survey_connection"]));

            services.AddTransient<DbContext, SurveyContext>();
            services.AddTransient<IUnitOfWork, EfUnitOfWork>();
            services.AddTransient<ISessionManagerFactory, SessionManagerFactory>();

            services.AddBAL();

            services.AddMvc();

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.CookieHttpOnly = true;
                options.CookieSecure = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {

            loggerFactory.AddConsole();
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}", defaults: new
                {
                    controller = "Home",
                    action = "index"
                });
            });

            if (env.IsDevelopment())
            {
                IAccountManager accountManager = serviceProvider.GetService<IAccountManager>();
                ISurveyManager surveyManager = serviceProvider.GetService<ISurveyManager>();
                string email = "yfozekosh@gmail.com";

                SeedDatabaseIfDevelop(accountManager, surveyManager, email);
            }
        }

        private static void SeedDatabaseIfDevelop(IAccountManager accountManager, ISurveyManager surveyManager, string email)
        {
            if (!accountManager.IsUserExists(email))
            {
                accountManager.RegisterUser("yfozekosh@gmail.com", "1234");
                surveyManager.CreateSurvey(new Survey()
                {
                    Title = "Title",
                    Questions = new List<Question>()
                        {
                            new CheckboxQuestion()
                            {
                                QuestionLabel = "What about checkboxes?",
                                Options = new List<QuestionOption>()
                                {
                                    new QuestionOption()
                                    {
                                        IsSelected = true,
                                        Value = "select1"
                                    },
                                    new QuestionOption()
                                    {
                                        IsSelected = false,
                                        Value = "select2"
                                    },
                                    new QuestionOption()
                                    {
                                        IsSelected = false,
                                        Value = "select3"
                                    }
                                }
                            },
                            new RadioQuestion()
                            {
                                QuestionLabel = "What about radio?",
                                Options = new List<QuestionOption>()
                                {
                                    new QuestionOption()
                                    {
                                        IsSelected = false,
                                        Value = "select1"
                                    },
                                    new QuestionOption()
                                    {
                                        IsSelected = false,
                                        Value = "select2"
                                    },
                                    new QuestionOption()
                                    {
                                        IsSelected = false,
                                        Value = "select3"
                                    }
                                }
                            },
                            new InputQuestion()
                            {
                                QuestionLabel = "What about input?"
                            }
                        }
                }, email);
            }
        }
    }
}