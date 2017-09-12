using System.Threading.Tasks;

namespace CoreQuizz.Queries.Contract
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery: IQuery<TResult>
    {
        Task<TResult> ExecuteAsync(TQuery query);

        TResult Execute(TQuery query);
    }
}