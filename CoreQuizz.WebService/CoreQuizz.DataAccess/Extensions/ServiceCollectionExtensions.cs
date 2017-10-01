using System;
using CoreQuizz.DataAccess.Contract.Contracts;
using CoreQuizz.DataAccess.DAL;
using CoreQuizz.DataAccess.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreQuizz.DataAccess.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDAL(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            if (configuration["db"] == "mssql")
            {
                services.AddDbContext<SurveyContext>(
                    options => options.UseSqlServer(configuration["survey_connection"]));
            } else if (configuration["db"] == "sqlite")
            {
                services.AddDbContext<SurveyContext>(
                    options => options.UseSqlite(configuration["sqlite_connection"]));
            }

            services.AddTransient<Microsoft.EntityFrameworkCore.DbContext, SurveyContext>();
            services.AddTransient<IUnitOfWork, EfUnitOfWork>();
        }
    }
}