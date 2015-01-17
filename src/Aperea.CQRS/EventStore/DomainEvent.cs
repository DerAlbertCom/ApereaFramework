using System;

namespace Aperea.CQRS.EventStore
{
    [Serializable]
    public abstract class DomainEvent : IDomainEvent
    {
        public DateTime ExecutedOn { get; private set; }
        public Guid Id { get; private set; }
        public Guid AggregateId { get; set; }
        long IDomainEvent.Version { get; set; }

        protected DomainEvent()
        {
            Id = Guid.NewGuid();
            ExecutedOn = DateTime.UtcNow;
        }
    }
}