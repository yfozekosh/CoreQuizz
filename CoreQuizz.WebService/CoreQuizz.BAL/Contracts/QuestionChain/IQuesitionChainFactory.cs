using CoreQuizz.DataAccess.Contract.Contracts;
using CoreQuizz.Shared.DomainModel;

namespace CoreQuizz.BAL.Contracts
{
    public interface IQuestionChainFactory
    {
        IQuestionFiller GetQuestionFiller();

        IQuestionChecker GetQuestionChecker();
    }
}