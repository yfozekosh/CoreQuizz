namespace CoreQuizz.Queries.Contract.Result
{
    public class OkQueryResult<TResult> : QueryResult<TResult>
    {
        public OkQueryResult(TResult value) : base(true)
        {
            Value = value;
        }
    }
}