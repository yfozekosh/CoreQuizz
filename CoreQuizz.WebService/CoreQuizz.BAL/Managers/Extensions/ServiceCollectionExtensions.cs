using CoreQuizz.BAL.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace CoreQuizz.BAL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBAL(this IServiceCollection services)
        {
            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<ISurveyManager, SurveyManager>();
            services.AddTransient<IQuestionManager, QuestionManager>();

            services.AddTransient<IQuestionChainFactory, QuestionChainFactory>();
        }
    }
}
