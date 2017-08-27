using System;
using CoreQuizz.BAL.Contracts;
using CoreQuizz.BAL.Managers.Extensions;
using CoreQuizz.DataAccess.Extensions;
using CoreQuizz.Queries.Extensions;
using CoreQuizz.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddDAL(Configuration);
            
            services.AddTransient<IDependencyResolver, AspNetCoreDependencyResolver>();

            services.AddBAL();
            services.AddQueries();

            services.AddMvc();
            services.AddSession();
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

            app.UseIdentityServer(Configuration);

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