namespace Aperea.Infrastructure.Bootstrap
{
    public interface IBootstrapItem
    {
        void Execute();
        int Order { get; }
    }
}