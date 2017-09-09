using System.Threading.Tasks;

namespace CoreQuizz.Commands.Contract
{
    public interface ICommandDispatcher
    {
        CommandResult Execute<TCommand>(TCommand command) where TCommand : ICommand;

        Task<CommandResult> ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}