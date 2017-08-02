using System.Collections.Generic;
using System.Linq;
using CoreQuizz.DataAccess.Contract.Contracts;
using CoreQuizz.Shared.DomainModel;
using CoreQuizz.BAL.Contracts;

namespace CoreQuizz.BAL
{
    public class QuestionManager : IQuestionManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuestionFiller _questionFiller;
        private readonly IQuestionChecker _questionChecker;

        public QuestionManager(IUnitOfWork unitOfWork, IQuestionChainFactory chainFactory)
        {
            _unitOfWork = unitOfWork;
            _questionFiller = chainFactory.GetQuestionFiller();
            _questionChecker = chainFactory.GetQuestionChecker();
        }

        public Question LoadFullQuestion(Question question, IRepository<QuestionOption> optionsRepository)
        {
            if (optionsRepository == null)
            {
                optionsRepository = _unitOfWork.GetRepository<QuestionOption>();
            }
            
            RadioQuestion radioQuestion = question as RadioQuestion;
            CheckboxQuestion checkboxQuestion = question as CheckboxQuestion;
            List<QuestionOption> options =
                optionsRepository.Get(option => option.Question != null && option.Question.Id == question.Id, option => option.Question).ToList();

            if (radioQuestion != null)
            {
                radioQuestion.Options = options;
            }

            if (checkboxQuestion != null)
            {
                checkboxQuestion.Options = options;
            }

            return question;
        }

        public IList<Question> LoadFullQuestions(IList<Question> questions)
        {
            return questions.Select(LoadFullQuestion).ToList();
        }

        public Question LoadFullQuestion(Question question)
        {
            return LoadFullQuestion(question, null);
        }

        public bool IsQuestionsFull(IList<Question> questions)
        {
            return _questionChecker.IsQuestionsFilled(questions);
        }
    }
}
