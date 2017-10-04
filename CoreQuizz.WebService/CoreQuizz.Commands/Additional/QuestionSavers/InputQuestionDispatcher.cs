using System;
using System.Threading.Tasks;
using CoreQuizz.Commands.Additional.QuestionSavers.Abstract;
using CoreQuizz.Commands.Contract;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Shared.DomainModel.Survey;
using CoreQuizz.Shared.DomainModel.Survey.Question;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CoreQuizz.Commands.Additional.QuestionSavers
{
    public class InputQuestionDispatcher : EfQuestionDispatcher<InputQuestion>
    {
        public InputQuestionDispatcher(SurveyContext surveyContext) : base(surveyContext)
        {
        }

        public override async Task<CommandResult> SaveAsync(Survey survey, InputQuestion question)
        {
            if (question == null) throw new ArgumentNullException(nameof(question));
            Question dbQuestion = await SurveyContext.FindAsync<Question>(question.Id);

            if (dbQuestion == null)
            {
                survey.Questions.Add(question);
                return new CommandResult(true);
            }

            var inputDbQuestion = dbQuestion as InputQuestion;
            if (inputDbQuestion == null)
            {
                SurveyContext.Entry(dbQuestion).State = EntityState.Deleted;
            }
            else
            {
                inputDbQuestion.Value = question.Value;
                inputDbQuestion.ModifieDateTime = DateTime.Now;
                inputDbQuestion.QuestionLabel = question.QuestionLabel;
            }

            return new CommandResult(true);
        }

        public override async Task<CommandResult> DeleteAsync(Survey survey, InputQuestion question)
        {
            if (question == null) throw new ArgumentNullException(nameof(question));
            InputQuestion dbQuestion = SurveyContext.Find<InputQuestion>(question.Id);

            if (dbQuestion == null)
            {
                throw new ArgumentException("Question do not exists");
            }

            SurveyContext.Entry(dbQuestion).State = EntityState.Deleted;

            return new CommandResult(true);
        }
    }
}