using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using CoreQuizz.Queries.Additional;
using CoreQuizz.Queries.Additional.Contracts;
using CoreQuizz.Queries.Contract;
using CoreQuizz.Queries.Exceptions;
using CoreQuizz.Queries.PageQueries;
using CoreQuizz.Queries.PageQueries.Handlers;
using CoreQuizz.Queries.PageQueries.Queries;
using CoreQuizz.Queries.PageQueries.Responces;
using Microsoft.Extensions.DependencyInjection;

namespace CoreQuizz.Queries.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddQueries(this IServiceCollection serviceCollection)
        {
            if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

            serviceCollection.AddTransient<IQueryDispatcher, QueryDispatcher>();

            RegisterQueries(serviceCollection);

            serviceCollection.AddTransient<IQuestionFetcherFactory, QuestionFetcherFactory>();
        }

        private static void RegisterQueries(IServiceCollection serviceCollection)
        {
            var assembly = typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly;
            IList<Type> handlerTypes;
            IList<Tuple<Type, Type>> queryQueryResultList = new List<Tuple<Type, Type>>();

            try
            {
                IList<Type> queryTypes = assembly.GetTypes().Where(type =>
                {
                    IEnumerable<Type> typeInterfaces = type.GetInterfaces().Where(i => i.GetTypeInfo().IsGenericType)
                        .Select(i => i.GetGenericTypeDefinition());

                    bool isQuery = typeInterfaces.Contains(typeof(IQuery<>));
                    return isQuery && !type.GetTypeInfo().IsAbstract;
                }).ToList();


                foreach (var queryType in queryTypes)
                {
                    IEnumerable<Type> queryGenericInterfaces = queryType
                        .GetInterfaces()
                        .Where(i => i.GetTypeInfo().IsGenericType);

                    Type queryResultType = queryGenericInterfaces
                        .Single(i => i.GetGenericTypeDefinition() == typeof(IQuery<>));

                    Tuple<Type, Type> queryQueryResult =
                        new Tuple<Type, Type>(queryType, queryResultType.GetGenericArguments().Single());

                    queryQueryResultList.Add(queryQueryResult);
                }

                handlerTypes = assembly.GetTypes().Where(type =>
                {
                    var isHandler = type.GetInterfaces().Where(i => i.GetTypeInfo().IsGenericType)
                        .Select(i => i.GetGenericTypeDefinition()).Contains(typeof(IQueryHandler<,>));
                    return isHandler && !type.GetTypeInfo().IsAbstract;
                }).ToList();
            }
            catch (Exception e)
            {
                throw new QueryTypeException("Cannot get query or handler types in assembly", e);
            }

            foreach (Tuple<Type, Type> tuple in queryQueryResultList)
            {
                Type handlerRealizationType;
                Type constructedQueryHandler = typeof(IQueryHandler<,>).MakeGenericType(tuple.Item1, tuple.Item2);

                try
                {
                    handlerRealizationType = handlerTypes.Single(t => t.GetInterfaces().Contains(constructedQueryHandler));
                }
                catch (Exception e)
                {
                    throw new QueryHandlerNotFoundException(tuple.Item1);
                }


                try
                {
                    serviceCollection.AddTransient(constructedQueryHandler, handlerRealizationType);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw new QueryTypeNotRegisteredException(constructedQueryHandler);
                }
            }
        }
    }
}