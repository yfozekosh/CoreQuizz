using System.Collections.Generic;
using CoreQuizz.BAL.Contracts;
using CoreQuizz.DataAccess.Contract.Contracts;
using CoreQuizz.BAL.Contracts.QuestionChain;
using CoreQuizz.BAL.QuestionChain.ChainElements;

namespace CoreQuizz.BAL
{
    public class QuestionChainFactory : IQuestionChainFactory
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionChainFactory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQuestionFiller GetQuestionFiller()
        {
            // TODO: change to reflection creation by namespace convention

            var chainElements = new List<IQuestionTypeChainElement>
            {
                new CheckoxQuestionChainElement(isCheckOnly: false,unitOfWork: _unitOfWork),
                new RadioQuestionChainElemnt(isCheckOnly: false,unitOfWork: _unitOfWork),
                new InputQuestionChainElement()
            };

            return new QuestionFiller(chainElements);
        }

        public IQuestionChecker GetQuestionChecker()
        {
            // TODO: change to reflection creation by namespace convention

            var chainElements = new List<IQuestionTypeChainElement>
            {
                new CheckoxQuestionChainElement(isCheckOnly: true,unitOfWork: _unitOfWork),
                new RadioQuestionChainElemnt(isCheckOnly: true,unitOfWork: _unitOfWork),
                new InputQuestionChainElement()
            };

            return new QuestionChecker(chainElements);
        }
    }
}
