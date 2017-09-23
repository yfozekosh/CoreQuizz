using System;
using System.Threading.Tasks;
using CoreQuizz.Commands.Contract;
using CoreQuizz.DataAccess.DbContext;
using Microsoft.Extensions.Logging;

namespace CoreQuizz.Commands.Handlers.Abstract
{
    public abstract class EfCommandHandler<T> : ICommandHandler<T> where T : ICommand
    {
        protected readonly SurveyContext SurveyContext;
        private readonly ILogger<EfCommandHandler<T>> _logger;

        protected EfCommandHandler(SurveyContext surveyContext, ILogger<EfCommandHandler<T>> logger)
        {
            SurveyContext = surveyContext;
            _logger = logger;
        }

        protected abstract Task<CommandResult> _ExecuteAsync(T command);

        public Task<CommandResult> ExecuteAsync(T command)
        {
            try
            {
                return _ExecuteAsync(command);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e.StackTrace);
                return Task.FromResult(new CommandResult(false)
                {
                    Exceptions = new[] {e},
                    Error = "Unexpected error."
                });
            }
        }

        public CommandResult Execute(T command)
        {
            return ExecuteAsync(command).GetAwaiter().GetResult();
        }
    }
}