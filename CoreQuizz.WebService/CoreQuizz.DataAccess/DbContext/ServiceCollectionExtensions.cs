using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreQuizz.DataAccess.DbContext
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEntityFramework(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SurveyContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}