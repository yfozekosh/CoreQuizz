using CoreQuizz.BAL.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace CoreQuizz.BAL.Managers.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBAL(this IServiceCollection services)
        {
            services.AddTransient<IAccountManager, AccountManager>();
        }
    }
}
