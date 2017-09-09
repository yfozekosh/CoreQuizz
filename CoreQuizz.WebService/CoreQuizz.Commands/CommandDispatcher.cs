using System;
using System.Threading.Tasks;
using CoreQuizz.Commands.Commands;
using CoreQuizz.Commands.Contract;
using CoreQuizz.Commands.Exceptions;
using CoreQuizz.Shared;
using Microsoft.Extensions.Logging;

namespace CoreQuizz.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IDependencyResolver _dependencyResolver;
        private readonly ILogger<CommandDispatcher> _logger;

        public CommandDispatcher(IDependencyResolver dependencyResolver, ILogger<CommandDispatcher> logger)
        {
            _dependencyResolver = dependencyResolver;
            _logger = logger;
        }

        public CommandResult Execute<TCommand>(TCommand command) where TCommand : ICommand
        {
            return ExecuteAsync(command).GetAwaiter().GetResult();
        }

        public async Task<CommandResult> ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            ICommandHandler<TCommand> commandHandler = _dependencyResolver.Resolve<ICommandHandler<TCommand>>();

            if (commandHandler == null)
            {
                throw new CommandHandlerNotCreatedException(typeof(ICommandHandler<TCommand>));
            }

            try
            {
                return await commandHandler.ExecuteAsync(command);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e.InnerException);
                return new CommandResult(false)
                {
                    Exceptions = new[] {e}
                };
            }
        }
    }
}