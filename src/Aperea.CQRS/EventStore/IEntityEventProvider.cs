using System;
using System.Collections.Generic;

namespace Aperea.CQRS.EventStore
{
    public interface IEntityEventProvider<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        void Clear();
        void LoadFromHistory(IEnumerable<TDomainEvent> domainEvents);
        void HookUpVersionProvider(Func<long> versionProvider);
        IEnumerable<TDomainEvent> GetChanges();
        Guid Id { get; }
    }
}