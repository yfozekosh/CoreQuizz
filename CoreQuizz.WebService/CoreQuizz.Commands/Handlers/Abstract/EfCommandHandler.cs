using System.Threading.Tasks;
using CoreQuizz.Commands.Contract;
using CoreQuizz.DataAccess.DbContext;

namespace CoreQuizz.Commands.Handlers.Abstract
{
    public abstract class EfCommandHandler<T> : ICommandHandler<T> where T : ICommand
    {
        protected readonly SurveyContext SurveyContext;

        protected EfCommandHandler(SurveyContext surveyContext)
        {
            SurveyContext = surveyContext;
        }

        public abstract Task<CommandResult> ExecuteAsync(T command);

        public CommandResult Execute(T command)
        {
            return ExecuteAsync(command).GetAwaiter().GetResult();
        }
    }
}