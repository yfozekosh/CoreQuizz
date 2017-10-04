using System;
using System.Threading.Tasks;
using CoreQuizz.Commands.Additional.Contracts;
using CoreQuizz.Commands.Contract;
using CoreQuizz.Shared.DomainModel.Survey;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;

namespace CoreQuizz.Commands.Additional.QuestionSavers.Abstract
{
    public abstract class QuestionDispatcher<TQuestion> : IQuestionDispatcher, IQuestionDispatcher<TQuestion>
        where TQuestion : Question
    {
        public Task<CommandResult> SaveAsync(Survey survey, Question question)
        {
            if (survey == null) throw new ArgumentNullException(nameof(survey));

            TQuestion castedQuestion = CastToitsType(question);

            return SaveAsync(survey, castedQuestion);
        }

        public Task<CommandResult> DeleteAsync(Survey survey, Question question)
        {
            if (survey == null) throw new ArgumentNullException(nameof(survey));

            TQuestion castedQuestion = CastToitsType(question);

            return DeleteAsync(survey, castedQuestion);
        }

        private static TQuestion CastToitsType(Question question)
        {
            if (question == null) throw new ArgumentNullException(nameof(question));

            TQuestion castedQuestion = question as TQuestion;

            if (castedQuestion == null)
            {
                throw new ArgumentException(
                    $"Question should be of type {typeof(TQuestion)} but got type {question.GetType()}");
            }

            return castedQuestion;
        }

        public abstract Task<CommandResult> SaveAsync(Survey survey, TQuestion question);

        public abstract Task<CommandResult> DeleteAsync(Survey survey, TQuestion question);
    }
}