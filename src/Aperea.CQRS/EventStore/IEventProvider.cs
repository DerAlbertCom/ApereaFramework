using System;
using System.Collections.Generic;

namespace Aperea.CQRS.EventStore
{
    public interface IEventProvider<TDomainEvent>   where TDomainEvent : IDomainEvent
    {
        void Clear();
        void LoadFromHistory(IEnumerable<TDomainEvent> domainEvents);
        Guid Id { get; }
        void UpdateVersion(long version);
        long Version { get; }
        IEnumerable<TDomainEvent> GetChanges();
    }
}