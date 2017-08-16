namespace CoreQuizz.DataAccess.Contract.Contracts
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        void Execute(TCommand command);
    }
}