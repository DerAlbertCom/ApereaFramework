namespace Aperea.Commands
{
    public interface IQueryHandler
    {
        object Query(object command);
    }

    public interface IQueryHandler<in TCommand, out TResult> : IQueryHandler where TCommand : IQueryCommand<TResult>
    {
        TResult Query(TCommand command);
    }
}