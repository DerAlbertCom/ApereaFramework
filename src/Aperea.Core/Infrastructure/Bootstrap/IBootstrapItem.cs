using System;

namespace Aperea.Infrastructure.Bootstrap
{
    public interface IBootstrapItem : IDisposable   
    {
        void Execute();
        int Order { get; }
    }
}