using System.Threading.Tasks;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Queries.Contract;

namespace CoreQuizz.Queries.PageQueries.Handlers.Abstract
{
    public abstract class EfQueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        protected readonly SurveyContext Context;

        protected EfQueryHandler(SurveyContext context)
        {
            Context = context;
        }

        public abstract Task<TResult> ExecuteAsync(TQuery query);

        public TResult Execute(TQuery query)
        {
            return ExecuteAsync(query).GetAwaiter().GetResult();
        }
    }
}