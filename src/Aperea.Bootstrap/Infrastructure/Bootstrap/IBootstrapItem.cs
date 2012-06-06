using System;

namespace Aperea.Infrastructure.Bootstrap
{
    public interface IBootstrapItem : IDisposable
    {
        void Execute();
        int Order { get; }
    }

    public abstract class BootstrapItem : IBootstrapItem
    {
        ~BootstrapItem()
        {
            Disposing(false);
        }

        public void Dispose()
        {
            Disposing(true);
        }

        protected virtual void Disposing(bool disposed)
        {
        }

        public abstract void Execute();

        public virtual int Order
        {
            get { return 0; }
        }
    }
}