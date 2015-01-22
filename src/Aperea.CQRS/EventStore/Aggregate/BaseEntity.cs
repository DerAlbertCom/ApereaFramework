using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Aperea.CQRS.EventStore.Aggregate
{
    public abstract class BaseEntity<TDomainEvent> : IEntityEventProvider<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public Guid Id { get; protected set; }
        private readonly Dictionary<Type, Action<TDomainEvent>> _events;
        private readonly List<TDomainEvent> _appliedEvents;
        private Func<long> _versionProvider;

        public BaseEntity()
        {
            _events = new Dictionary<Type, Action<TDomainEvent>>();
            _appliedEvents = new List<TDomainEvent>();
        }

        protected void RegisterEvent<TEvent>(Action<TEvent> eventHandler) where TEvent : class, TDomainEvent
        {
            _events.Add(typeof(TEvent), theEvent => eventHandler(theEvent as TEvent));
        }

        protected void Apply<TEvent>(TEvent domainEvent) where TEvent : class, TDomainEvent
        {
            domainEvent.AggregateId = Id;
            domainEvent.Version = _versionProvider();
            apply(domainEvent.GetType(), domainEvent);
            _appliedEvents.Add(domainEvent);
        }

        void IEntityEventProvider<TDomainEvent>.LoadFromHistory(IEnumerable<TDomainEvent> domainEvents)
        {
            if (!domainEvents.Any())
                return;

            foreach (var domainEvent in domainEvents)
            {
                apply(domainEvent.GetType(), domainEvent);
            }
        }

        public void HookUpVersionProvider(Func<long> versionProvider)
        {
            _versionProvider = versionProvider;
        }

        IEnumerable<TDomainEvent> IEntityEventProvider<TDomainEvent>.GetChanges()
        {
            return _appliedEvents;
        }

        void IEntityEventProvider<TDomainEvent>.Clear()
        {
            _appliedEvents.Clear();
        }

        private void apply(Type eventType, TDomainEvent domainEvent)
        {
            Action<TDomainEvent> handler;

            if (!_events.TryGetValue(eventType, out handler))
                throw new UnregisteredDomainEventException(string.Format("The requested domain event '{0}' is not registered in '{1}'", eventType.FullName, GetType().FullName));

            handler(domainEvent);
        }
    }
}