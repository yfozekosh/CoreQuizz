using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreQuizz.Commands.Additional.Contracts;
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
        private readonly IQuestionSaverFactory _saverFactory;

        public UpdateSurveyHandler(SurveyContext surveyContext, IQuestionSaverFactory saverFactory,
            ILogger<EfCommandHandler<UpdateSurveyCommand>> logger) :
            base(surveyContext, logger)
        {
            _saverFactory = saverFactory;
        }

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

            await SurveyContext.Entry(dbSurvey).Collection(x => x.Questions).LoadAsync();

            var newIds = survey.Questions.Select(q => q.Id).ToArray();
            var dbQuestionIds = dbSurvey.Questions?.Select(q => q.Id).ToArray();
            var newQuestions = survey.Questions?.Where(q => dbQuestionIds == null || !dbQuestionIds.Contains(q.Id));
            var questionToUpdate = survey.Questions?.Where(q => dbQuestionIds != null && dbQuestionIds.Contains(q.Id));
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
                
                foreach (var question in questionToUpdate)
                {
                    CommandResult res = await this._saverFactory.GetSaver(question).SaveAsync(dbSurvey, question);
                    if (!res.IsSuccess)
                    {
                        return new CommandResult(false)
                        {
                            Error = "Something went wrong while updating question"
                        };
                    }
                }
            }


            try
            {
                await SurveyContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return new CommandResult(true);
        }
    }
}