using System;
using System.Collections.Generic;

namespace Aperea.CQRS.EventStore.Storage
{
    public interface IDomainEventStorage<TDomainEvent> : ITransactional where TDomainEvent : IDomainEvent
    {
        IEnumerable<TDomainEvent> GetAllEvents(Guid eventProviderId);
        IEnumerable<TDomainEvent> GetEventsSinceLastSnapShot(Guid eventProviderId, Type typeAgregateRoot);
        int GetEventCountSinceLastSnapShot(Guid eventProviderId);
        void Save(IEventProvider<TDomainEvent> eventProvider);

        TAggregate FindById<TAggregate>(Guid id)
            where TAggregate : class, IEventProvider<TDomainEvent>;
    }
}