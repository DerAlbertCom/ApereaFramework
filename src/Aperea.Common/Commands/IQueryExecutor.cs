namespace Aperea.Commands
{
    public interface IQueryExecutor
    {
        TResult ExecuteQuery<TResult>(IQueryCommand<TResult> command);
    }
}