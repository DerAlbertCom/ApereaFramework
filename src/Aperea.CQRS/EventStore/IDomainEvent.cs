using System;

namespace Aperea.CQRS.EventStore
{
    public interface IDomainEvent
    {
        DateTime ExecutedOn { get; }
        Guid Id { get; }
        Guid AggregateId { get; set; }
        long Version { get; set; }
    }
}