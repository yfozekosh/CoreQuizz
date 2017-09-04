﻿using System;
using CoreQuizz.Queries.Contract;
using CoreQuizz.Queries.PageQueries;
using Microsoft.Extensions.DependencyInjection;

namespace CoreQuizz.Queries.Extensions
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