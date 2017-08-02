using CoreQuizz.BAL.Contracts.QuestionChain;
using CoreQuizz.Shared.DomainModel;

namespace CoreQuizz.BAL.QuestionChain
{
    internal abstract class QuestionTypeChainElement : ChainElement<Question, QustionTypeChainResult>, IQuestionTypeChainElement
    {

    }
}