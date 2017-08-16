using System;
using CoreQuizz.DataAccess.Contract.Contracts;
using CoreQuizz.Queries.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace CoreQuizz.Queries.PageQueries.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddQueries(this IServiceCollection serviceCollection)
        {
            if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

            serviceCollection.AddTransient<IQueryDispatcher, QueryDispatcher>();
            serviceCollection.AddTransient<IQueryHandler<SurveyListPageQuery, SurveyListItem[]>,
                SurveyListPageQueryHandler>();
        }
    }
}