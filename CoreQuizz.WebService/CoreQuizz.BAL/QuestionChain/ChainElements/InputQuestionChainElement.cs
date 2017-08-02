using CoreQuizz.Shared.DomainModel;

namespace CoreQuizz.BAL.QuestionChain
{
    internal class InputQuestionChainElement : QuestionTypeChainElement
    {
        protected override ChainResult<QustionTypeChainResult> _Run(Question args)
        {
            if (args is InputQuestion)
            {
                return new ChainResult<QustionTypeChainResult>()
                {
                    Result = new QustionTypeChainResult()
                    {
                        Question = args,
                        IsFilled = true
                    },
                    IsResultFound = true
                };
            }
            return new ChainResult<QustionTypeChainResult>()
            {
                IsResultFound = false
            };
        }
    }
}