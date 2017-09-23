using System.Threading.Tasks;
using CoreQuizz.Queries.Contract.Result;

namespace CoreQuizz.Queries.Contract
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery: IQuery<TResult>
    {
        Task<QueryResult<TResult>> ExecuteAsync(TQuery query);

        QueryResult<TResult> Execute(TQuery query);
    }
}