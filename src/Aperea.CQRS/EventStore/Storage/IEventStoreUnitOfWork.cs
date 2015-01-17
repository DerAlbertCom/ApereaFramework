using System;

namespace Aperea.CQRS.EventStore.Storage
{
    public interface IEventStoreUnitOfWork<TDomainEvent> : IUnitOfWork where TDomainEvent : IDomainEvent
    {
        TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IEventProvider<TDomainEvent>, new();
        void Add<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IEventProvider<TDomainEvent>, new();
        void RegisterForTracking<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IEventProvider<TDomainEvent>, new();
    }
}