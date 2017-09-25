using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreQuizz.Commands.Commands;
using CoreQuizz.Commands.Contract;
using CoreQuizz.Commands.Handlers.Abstract;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Shared.DomainModel.Survey;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreQuizz.Commands.Handlers
{
    public class UpdateSurveyHandler : EfCommandHandler<UpdateSurveyCommand>
    {
        protected override async Task<CommandResult> _ExecuteAsync(UpdateSurveyCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            Survey survey = command.Survey;

            Survey dbSurvey = await SurveyContext.Surveys.FindAsync(survey.Id);
            if (dbSurvey == null)
            {
                return new CommandResult(false)
                {
                    Error = "No such survey in Db"
                };
            }

            var newIds = survey.Questions.Select(q => q.Id).ToArray();
            var dbQuestionIds = dbSurvey.Questions?.Select(q => q.Id).ToArray();
            var newQuestions = survey.Questions?.Where(q => dbQuestionIds == null || !dbQuestionIds.Contains(q.Id));

            if (dbSurvey.Questions != null)
            {
                foreach (var dbSurveyQuestion in dbSurvey.Questions)
                {
                    if (!newIds.Contains(dbSurveyQuestion.Id))
                    {
                        SurveyContext.Entry(dbSurveyQuestion).State = EntityState.Deleted;
                    }
                }
            }
            
            dbSurvey.Title = survey.Title;
            dbSurvey.Description = survey.Description;
            dbSurvey.SurveyPassAccessLevel = survey.SurveyPassAccessLevel;
            dbSurvey.ModifieDateTime = DateTime.Now;
            if (newQuestions != null)
            {
                if (dbSurvey.Questions == null)
                {
                    dbSurvey.Questions = new List<Question>();
                }
                
                foreach (var newQuestion in newQuestions)
                {
                    dbSurvey.Questions.Add(newQuestion);
                }
            }


            await SurveyContext.SaveChangesAsync();

            return new CommandResult(true);
        }

        public UpdateSurveyHandler(SurveyContext surveyContext, ILogger<EfCommandHandler<UpdateSurveyCommand>> logger) :
            base(surveyContext, logger)
        {
        }
    }
}