namespace CoreQuizz.DataAccess.Contract.Contracts
{
    public interface ICommand
    {
    }

    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        void Execute(TCommand command);
    }

    public interface ICommandDispatcher
    {
        void Execute<TCommand>(TCommand command) where TCommand : ICommand;
    }

    public interface IQuery<TResult>
    {

    }

    public interface IQueryHandler<in TQuery, out TResult> where TQuery: IQuery<TResult>
    {
        TResult Execute(TQuery query);
    }

    public interface IQueryDispatcher
    {
        TResult Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}
