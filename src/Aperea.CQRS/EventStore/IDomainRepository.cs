using System;

namespace Aperea.CQRS.EventStore
{
    public interface IDomainRepository<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        TAggregate GetById<TAggregate>(Guid id)
            where TAggregate : class,IEventProvider<TDomainEvent>, new();

        void Add<TAggregate>(TAggregate aggregateRoot)
            where TAggregate : class,IEventProvider<TDomainEvent>, new();
    }
}