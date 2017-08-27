namespace CoreQuizz.BAL.QuestionChain
{
    public class ChainResult<TResult>
    {
        public bool IsResultFound { get; set; } = true;

        public TResult Result { get; set; }
    }
}