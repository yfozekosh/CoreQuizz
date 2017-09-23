using System;
using System.Threading.Tasks;
using CoreQuizz.Commands.Commands;
using CoreQuizz.Commands.Contract;
using CoreQuizz.Commands.Handlers.Abstract;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Shared.DomainModel.Survey;
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

            return new CommandResult(true);
        }

        public UpdateSurveyHandler(SurveyContext surveyContext, ILogger<EfCommandHandler<UpdateSurveyCommand>> logger) : base(surveyContext, logger)
        {
        }
    }
}