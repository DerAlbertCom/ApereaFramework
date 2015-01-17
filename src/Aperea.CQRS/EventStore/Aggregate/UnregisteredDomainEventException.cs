using System;

namespace Aperea.CQRS.EventStore.Aggregate
{
    public class UnregisteredDomainEventException : Exception
    {
        public UnregisteredDomainEventException(string message) : base(message) {}
    }
}