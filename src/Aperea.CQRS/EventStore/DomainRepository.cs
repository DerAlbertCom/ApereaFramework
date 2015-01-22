using System;
using Aperea.CQRS.EventStore.Storage;

namespace Aperea.CQRS.EventStore
{
    public class DomainRepository<TDomainEvent> : IDomainRepository<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        private readonly IEventStoreUnitOfWork<TDomainEvent> _eventStoreUnitOfWork;
        private readonly IIdentityMap<TDomainEvent> _identityMap;

        public DomainRepository(IEventStoreUnitOfWork<TDomainEvent> eventStoreUnitOfWork, IIdentityMap<TDomainEvent> identityMap)
        {
            _eventStoreUnitOfWork = eventStoreUnitOfWork;
            _identityMap = identityMap;
        }

        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IEventProvider<TDomainEvent>, new()
        {
            return RegisterForTracking(_identityMap.GetById<TAggregate>(id)) ?? _eventStoreUnitOfWork.GetById<TAggregate>(id);
        }

        public void Add<TAggregate>(TAggregate aggregateRoot) where TAggregate : class,  IEventProvider<TDomainEvent>, new()
        {
            _eventStoreUnitOfWork.Add(aggregateRoot);
        }

        private TAggregate RegisterForTracking<TAggregate>(TAggregate aggregateRoot) where TAggregate : class,  IEventProvider<TDomainEvent>, new()
        {
            if (aggregateRoot == null)
                return aggregateRoot;

            _eventStoreUnitOfWork.RegisterForTracking(aggregateRoot);
            return aggregateRoot;
        }
    }
}