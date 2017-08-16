namespace CoreQuizz.DataAccess.Contract.Contracts
{
    public interface ICommandDispatcher
    {
        void Execute<TCommand>(TCommand command) where TCommand : ICommand;
    }
}