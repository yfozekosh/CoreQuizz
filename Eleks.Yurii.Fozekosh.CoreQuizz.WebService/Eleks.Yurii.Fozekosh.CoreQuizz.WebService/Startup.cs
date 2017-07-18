using System;
using Eleks.Yurii.Fozekosh.CoreQuizz.BAL;
using Eleks.Yurii.Fozekosh.CoreQuizz.BAL.Contracts;
using Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.Contracts;
using Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.DbContext;
using Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.EfDAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.WebService
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
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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
        }
    }
}