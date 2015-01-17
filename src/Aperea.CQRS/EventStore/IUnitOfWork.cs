
namespace Aperea.CQRS.EventStore
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}