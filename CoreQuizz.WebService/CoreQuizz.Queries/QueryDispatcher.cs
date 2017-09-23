using System;
using System.Threading.Tasks;
using CoreQuizz.Queries.Contract;
using CoreQuizz.Queries.Contract.Result;
using CoreQuizz.Queries.Exceptions;
using CoreQuizz.Shared;

namespace CoreQuizz.Queries
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IDependencyResolver _dependencyResolver;

        public QueryDispatcher(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public async Task<QueryResult<TResult>> ExecuteAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            var handler = _dependencyResolver.Resolve<IQueryHandler<TQuery, TResult>>();

            if (handler == null) throw new QueryHandlerNotFoundException(typeof(TQuery));

            QueryResult<TResult> queryResult;

            try
            {
                queryResult = await handler.ExecuteAsync(query);
            }
            catch (Exception e)
            {
                queryResult = new QueryResult<TResult>(false)
                {
                    Exceptions = new[]
                    {
                        e
                    },
                    Error = "Unexpected error occured during request. Please try later, or contact support team."
                };
            }

            return queryResult;
        }

        public QueryResult<TResult> Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            return this.ExecuteAsync<TQuery, TResult>(query).GetAwaiter().GetResult();
        }
    }
}