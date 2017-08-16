namespace CoreQuizz.DataAccess.Contract.Contracts
{
    public interface IQueryHandler<in TQuery, out TResult> where TQuery: IQuery<TResult>
    {
        TResult Execute(TQuery query);
    }
}