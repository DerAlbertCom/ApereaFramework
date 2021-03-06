using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Aperea.CQRS.EventStore.Aggregate
{
    public abstract class BaseAggregateRoot<TDomainEvent> : IEventProvider<TDomainEvent>, IRegisterChildEntities<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        private readonly Dictionary<Type, Action<TDomainEvent>> _registeredEvents;
        private readonly List<TDomainEvent> _appliedEvents;
        private readonly List<IEntityEventProvider<TDomainEvent>> _childEventProviders;

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public Guid Id { get; protected set; }
        public long Version { get; protected set; }
        public long EventVersion { get; protected set; }

        protected BaseAggregateRoot()
        {
            _registeredEvents = new Dictionary<Type, Action<TDomainEvent>>();
            _appliedEvents = new List<TDomainEvent>();
            _childEventProviders = new List<IEntityEventProvider<TDomainEvent>>();
        }

        protected void RegisterEvent<TEvent>(Action<TEvent> eventHandler) where TEvent : class, TDomainEvent
        {
            _registeredEvents.Add(typeof(TEvent), theEvent => eventHandler(theEvent as TEvent));
        }

        protected void Apply<TEvent>(TEvent domainEvent) where TEvent : class, TDomainEvent
        {
            domainEvent.AggregateId = Id;
            domainEvent.Version = GetNewEventVersion();
            Apply(domainEvent.GetType(), domainEvent);
            _appliedEvents.Add(domainEvent);
        }

        void IEventProvider<TDomainEvent>.LoadFromHistory(IEnumerable<TDomainEvent> domainEvents)
        {
            var events = domainEvents.ToArray();
            if (domainEvents == null || !events.Any())
                return;

            foreach (var domainEvent in events)
            {
                Apply(domainEvent.GetType(), domainEvent);
            }

            Version = events.Last().Version;
            EventVersion = Version;
        }

        private void Apply(Type eventType, TDomainEvent domainEvent)
        {
            Action<TDomainEvent> handler;

            if (!_registeredEvents.TryGetValue(eventType, out handler))
                throw new UnregisteredDomainEventException(string.Format("The requested domain event '{0}' is not registered in '{1}'", eventType.FullName, GetType().FullName));
            handler(domainEvent);
            Version = domainEvent.Version;
        }

        IEnumerable<TDomainEvent> IEventProvider<TDomainEvent>.GetChanges()
        {
            return _appliedEvents.Concat(GetChildEventsAndUpdateEventVersion()).OrderBy(x => x.Version).ToList();
        }

        void IEventProvider<TDomainEvent>.Clear()
        {
            _childEventProviders.ForEach(x => x.Clear());
            _appliedEvents.Clear();
        }

        void IEventProvider<TDomainEvent>.UpdateVersion(long version)
        {
            Version = version;
        }

        void IRegisterChildEntities<TDomainEvent>.RegisterChildEventProvider(IEntityEventProvider<TDomainEvent> entityEventProvider)
        {
            entityEventProvider.HookUpVersionProvider(GetNewEventVersion);
            _childEventProviders.Add(entityEventProvider);
        }

        private IEnumerable<TDomainEvent> GetChildEventsAndUpdateEventVersion()
        {
            return _childEventProviders.SelectMany(entity => entity.GetChanges());
        }

        private long GetNewEventVersion()
        {
            return ++EventVersion;
        }
    }
}