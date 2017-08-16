using CoreQuizz.DataAccess.Contract.Contracts;

namespace CoreQuizz.Queries.Contracts
{
    public interface IQueryDispatcher
    {
        TResult Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}