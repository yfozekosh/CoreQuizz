namespace CoreQuizz.Queries.Contract.Result
{
    public class ErrorQueryResult<TResult> : QueryResult<TResult>
    {
        public ErrorQueryResult(string error) : base(false)
        {
            Error = error;
        }
    }
}