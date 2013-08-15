namespace Aperea.Commands
{
    public abstract class QueryHandler<TCommand, TResult> : IQueryHandler<TCommand, TResult>
        where TCommand : IQueryCommand<TResult>
    {
        public abstract TResult Query(TCommand command);

        object IQueryHandler.Query(object command)
        {
            return Query((TCommand) command);
        }
    }
}