using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Library.Infrastructure
{
    public class Event<TAggregateId>
        where TAggregateId : IAggregateId
    {
        public IDomainEvent<TAggregateId> DomainEvent { get; }
        public long EventNumber { get; }

        public Event(IDomainEvent<TAggregateId> domainEvent, long eventNumber)
        {
            DomainEvent = domainEvent;
            EventNumber = eventNumber;
        }
    }
}