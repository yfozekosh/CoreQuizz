using System;
using System.Threading.Tasks;
using CoreQuizz.Commands.Additional.Contracts;
using CoreQuizz.Commands.Contract;
using CoreQuizz.Shared.DomainModel.Survey;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;

namespace CoreQuizz.Commands.Additional.QuestionSavers.Abstract
{
    public abstract class QuestionSaver<TQuestion> : IQuestionSaver, IQuestionSaver<TQuestion>
        where TQuestion : Question
    {
        public Task<CommandResult> SaveAsync(Survey survey, Question question)
        {
            if (survey == null) throw new ArgumentNullException(nameof(survey));
            if (question == null) throw new ArgumentNullException(nameof(question));

            TQuestion castedQuestion = question as TQuestion;

            if (castedQuestion == null)
            {
                throw new ArgumentException(
                    $"Question should be of type {typeof(TQuestion)} but got type {question.GetType()}");
            }

            return SaveAsync(survey, castedQuestion);
        }

        public abstract Task<CommandResult> SaveAsync(Survey survey, TQuestion question);
    }
}