using System;
using System.Collections.Generic;
using CoreQuizz.BAL.Contracts;
using CoreQuizz.BAL.Contracts.QuestionChain;
using CoreQuizz.Shared.DomainModel;

namespace CoreQuizz.BAL
{
    internal class QuestionChecker : IQuestionChecker
    {
        private readonly IList<IQuestionTypeChainElement> _chainElements;

        public QuestionChecker(IList<IQuestionTypeChainElement> chainElements)
        {
            if (chainElements == null) throw new ArgumentNullException(nameof(chainElements));

            _chainElements = chainElements;
        }

        public bool IsQuestionsFilled(IList<Question> questions)
        {
            if (questions == null) throw new ArgumentNullException(nameof(questions));

            bool result = true;

            foreach (Question question in questions)
            {
                foreach (IQuestionTypeChainElement questionTypeChainElement in _chainElements)
                {
                    result = questionTypeChainElement.Run(question).IsFilled;
                    if (!result) break;
                }
                if (!result) break;
            }

            return result;
        }
    }
}