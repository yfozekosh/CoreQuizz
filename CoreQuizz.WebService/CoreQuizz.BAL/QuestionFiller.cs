using System;
using System.Collections.Generic;
using CoreQuizz.BAL.Contracts;
using CoreQuizz.BAL.Contracts.QuestionChain;
using CoreQuizz.BAL.QuestionChain;
using CoreQuizz.Shared.DomainModel;

namespace CoreQuizz.BAL
{
    internal class QuestionFiller : IQuestionFiller
    {
        private readonly IList<IQuestionTypeChainElement> _chainElements;

        public QuestionFiller(IList<IQuestionTypeChainElement> chainElements)
        {
            if (chainElements == null) throw new ArgumentNullException(nameof(chainElements));

            _chainElements = chainElements;
        }

        public IList<Question> Fill(IList<Question> questions)
        {
            if (questions == null) throw new ArgumentNullException(nameof(questions));

            IList<Question> resultList = new List<Question>();

            foreach (Question question in questions)
            {
                foreach (IQuestionTypeChainElement chainElement in _chainElements)
                {
                    resultList.Add(chainElement.Run(question).Question);
                }
            }

            return resultList;
        }
    }
}