namespace CoreQuizz.BAL.Contracts.QuestionChain
{
    public interface IQuestionChainFactory
    {
        IQuestionFiller GetQuestionFiller();

        IQuestionChecker GetQuestionChecker();
    }
}