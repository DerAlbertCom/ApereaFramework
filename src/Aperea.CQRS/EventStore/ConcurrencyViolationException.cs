using System;

namespace Aperea.CQRS.EventStore
{
    public class ConcurrencyViolationException : Exception
    {
        public ConcurrencyViolationException(IEventProvider<IDomainEvent> eventProvider):base(CreateMessage(eventProvider))
        {
        }

        private static string CreateMessage(IEventProvider<IDomainEvent> eventProvider)
        {
            return string.Format("The AggregateRoot {0} of Type {1} was changed",eventProvider.Id,eventProvider.GetType().FullName);
        }
    }
}