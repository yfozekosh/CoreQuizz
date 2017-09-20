using System.Threading.Tasks;
using CoreQuizz.Queries.Contract.Result;

namespace CoreQuizz.Queries.Contract
{
    public interface IQueryDispatcher
    {
        Task<QueryResult<TResult>> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
        
        QueryResult<TResult> Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}