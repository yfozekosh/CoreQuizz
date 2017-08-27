using CoreQuizz.Shared.DomainModel;

namespace CoreQuizz.BAL.QuestionChain
{
    internal class QustionTypeChainResult
    {
        public bool IsFilled { get; set; }
        public Question Question { get; set; }
    }
}