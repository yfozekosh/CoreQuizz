using System;
using CoreQuizz.DataAccess.Contract.Contracts;
using CoreQuizz.Queries.Contracts;
using CoreQuizz.Queries.PageQueries;
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
            //var surveyRepo = _dependencyResolver.Resolve<IUnitOfWork>();
            //var handler = new SurveyListPageQueryHandler(surveyRepo);
            var handler = _dependencyResolver.Resolve<IQueryHandler<TQuery, TResult>>();

            if (handler == null) throw new QueryHandlerNotFoundException(typeof(TQuery));
            return handler.Execute(query);
        }
    }
}