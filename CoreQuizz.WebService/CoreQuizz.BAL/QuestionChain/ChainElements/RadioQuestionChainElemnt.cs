using System;
using System.Linq;
using CoreQuizz.DataAccess.Contract.Contracts;
using CoreQuizz.Shared.DomainModel;

namespace CoreQuizz.BAL.QuestionChain.ChainElements
{
    internal class RadioQuestionChainElemnt : QuestionTypeChainElement
    {
        private readonly bool _isCheckOnly;
        private readonly IUnitOfWork _unitOfWork;

        public RadioQuestionChainElemnt(bool isCheckOnly, IUnitOfWork unitOfWork)
        {
            _isCheckOnly = isCheckOnly;
            _unitOfWork = unitOfWork;
        }

        protected override ChainResult<QustionTypeChainResult> _Run(Question args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));

            RadioQuestion question = args as RadioQuestion;
            var result = new ChainResult<QustionTypeChainResult>()
            {
                IsResultFound = false,
                Result = null
            };

            if (question == null)
            {
                return result;
            }

            if (question.Options != null)
            {
                result.IsResultFound = true;

                if (_isCheckOnly)
                {
                    result.Result = new QustionTypeChainResult()
                    {
                        IsFilled = true
                    };
                }

                IRepository<QuestionOption> optionRepository = _unitOfWork.GetRepository<QuestionOption>();
                question.Options = optionRepository.Get(option => option.Question.Id == args.Id, include => include.Question).ToList();

                result.Result = new QustionTypeChainResult()
                {
                    Question = question,
                    IsFilled = true
                };
            }
            return result;
        }
    }
}