namespace CoreQuizz.BAL
{
    public class ChainResult<TResult>
    {
        public bool IsResultFound { get; set; } = true;

        public TResult Result { get; set; }
    }
}