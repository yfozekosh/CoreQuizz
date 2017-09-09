using System.Collections;
using System.Threading.Tasks;

namespace CoreQuizz.Commands.Contract
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        CommandResult Execute(TCommand command);

        Task<CommandResult> ExecuteAsync(TCommand command);
    }
}