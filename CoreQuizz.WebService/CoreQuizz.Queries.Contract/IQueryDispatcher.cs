using System.Threading.Tasks;

namespace CoreQuizz.Queries.Contract
{
    public interface IQueryDispatcher
    {
        Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
        TResult Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}