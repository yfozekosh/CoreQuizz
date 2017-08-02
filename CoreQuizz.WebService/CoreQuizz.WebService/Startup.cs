using System;
using System.Collections.Generic;
using CoreQuizz.BAL;
using CoreQuizz.BAL.Contracts;
using CoreQuizz.DataAccess.Contract.Contracts;
using CoreQuizz.DataAccess.DAL;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Shared.DomainModel;
using CoreQuizz.WebService.Session;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace CoreQuizz.WebService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            env.ConfigureNLog("nlog.config");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.CookieHttpOnly = true;
            });

            var connection = @"Server=(localdb)\mssqllocaldb;Database=CoreQuizz.SurveyDB;Trusted_Connection=True;";
            services.AddDbContext<SurveyContext>(options => options.UseSqlServer(connection));
            services.AddTransient<DbContext,SurveyContext>();
            services.AddTransient<IUnitOfWork, EfUnitOfWork>();            

            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<ISurveyManager, SurveyManager>();
            services.AddTransient<IQuestionManager, QuestionManager>();

            services.AddTransient<ISessionManagerFactory, SessionManagerFactory>();

            services.AddTransient<IQuestionChainFactory, QuestionChainFactory>();
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
}