using System;
using CoreQuizz.Queries.Contract;
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

        public TResult Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            var handler = _dependencyResolver.Resolve<IQueryHandler<TQuery, TResult>>();

            if (handler == null) throw new QueryHandlerNotFoundException(typeof(TQuery));
            return handler.Execute(query);
        }
    }
}