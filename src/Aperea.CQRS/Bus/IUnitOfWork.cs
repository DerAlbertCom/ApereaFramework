namespace Aperea.CQRS.Bus
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}