namespace CoreQuizz.BAL.Contracts
{
    public interface IQuestionChainFactory
    {
        IQuestionFiller GetQuestionFiller();

        IQuestionChecker GetQuestionChecker();
    }
}