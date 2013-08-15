namespace Aperea.Commands
{
    public interface IQueryDispatcher
    {
        IQueryHandler GetHandler(IQueryCommand query);
    }
}