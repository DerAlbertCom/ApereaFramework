using System.Collections.Generic;

namespace Aperea.CQRS.Bus
{
    public interface IBus : IUnitOfWork
    {
        void Publish(object message);
        void Publish(IEnumerable<object> messages);
    }
}