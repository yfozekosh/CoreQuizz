using CoreQuizz.BAL.Contracts;
using CoreQuizz.BAL.Contracts.QuestionChain;
using Microsoft.Extensions.DependencyInjection;

namespace CoreQuizz.BAL.Managers.Extensions
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
