using System;
using System.Collections.Generic;
using System.Linq;
using Aperea.CQRS.Bus;

namespace Aperea.CQRS.EventStore.Storage
{
    public class EventStoreUnitOfWork<TDomainEvent> : IEventStoreUnitOfWork<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        private readonly IDomainEventStorage<TDomainEvent> _domainEventStorage;
        private readonly IIdentityMap<TDomainEvent> _identityMap;
        private readonly IBus _bus;
        private readonly List<IEventProvider<TDomainEvent>> _eventProviders;

        public EventStoreUnitOfWork(IDomainEventStorage<TDomainEvent> domainEventStorage, IIdentityMap<TDomainEvent> identityMap, IBus bus)
        {
            _domainEventStorage = domainEventStorage;
            _identityMap = identityMap;
            _bus = bus;
            _eventProviders = new List<IEventProvider<TDomainEvent>>();
        }

        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IEventProvider<TDomainEvent>, new()
        {
            var aggregateRoot  = _domainEventStorage.FindById<TAggregate>(id) ?? new TAggregate();

            LoadRemainingHistoryEvents(id, aggregateRoot);

            RegisterForTracking(aggregateRoot);

            return aggregateRoot;
        }

        public void Add<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IEventProvider<TDomainEvent>, new()
        {
            RegisterForTracking(aggregateRoot);
        }

        public void RegisterForTracking<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IEventProvider<TDomainEvent>, new()
        {
            _eventProviders.Add(aggregateRoot);
            _identityMap.Add(aggregateRoot);
        }

        public void Commit()
        {
            _domainEventStorage.BeginTransaction();

            foreach (var eventProvider in _eventProviders)
            {
                _domainEventStorage.Save(eventProvider);
                _bus.Publish(eventProvider.GetChanges().Select(x => (object)x));
                eventProvider.Clear();
            }
            _eventProviders.Clear();

            _bus.Commit();
            _domainEventStorage.Commit();
        }

        public void Rollback()
        {
            _bus.Rollback();
            _domainEventStorage.Rollback();
            foreach (var eventProvider in _eventProviders)
            {
                _identityMap.Remove(eventProvider.GetType(), eventProvider.Id);
            }
            _eventProviders.Clear();
        }


        private void LoadRemainingHistoryEvents(Guid id, IEventProvider<TDomainEvent> aggregateRoot)
        {
            var events = _domainEventStorage.GetEventsSinceLastSnapShot(id, aggregateRoot.GetType());
            var domainEvents = events as TDomainEvent[] ?? events.ToArray();
            if (domainEvents.Any())
            {
                aggregateRoot.LoadFromHistory(domainEvents);
                return;
            }

            aggregateRoot.LoadFromHistory(_domainEventStorage.GetAllEvents(id));
        }
    }
}