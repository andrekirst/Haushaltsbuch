using System.Collections.Generic;
using System.Linq;

namespace Haushaltsbuch.Library.Infrastructure.Interfaces
{
    public abstract class AggregateBase<TAggregateId> :
        IAggregate<TAggregateId>,
        IEventSourcingAggregate<TAggregateId>
        where TAggregateId : IAggregateId
    {
        public const long NewAggregateVersion = -1;
        private readonly ICollection<IDomainEvent<TAggregateId>> _uncommitedEvents = new LinkedList<IDomainEvent<TAggregateId>>();
        private long _version = NewAggregateVersion;

        public TAggregateId Id { get; protected set; }

        long IEventSourcingAggregate<TAggregateId>.Version => _version;

        void IEventSourcingAggregate<TAggregateId>.ApplyEvent(IDomainEvent<TAggregateId> @event, long version)
        {
            if (!_uncommitedEvents.Any(predicate: x => Equals(objA: x.EventId, objB: @event.EventId)))
            {
                ((dynamic)this).ApplyEvent((dynamic)@event);
                _version = version;
            }
        }

        IEnumerable<IDomainEvent<TAggregateId>> IEventSourcingAggregate<TAggregateId>.GetUncommittedEvents()
        {
            return _uncommitedEvents.AsEnumerable();
        }

        void IEventSourcingAggregate<TAggregateId>.ClearUncommittedEvents()
        {
            _uncommitedEvents.Clear();
        }

        protected void RaiseEventIf<TEvent>(TEvent @event, bool expression)
            where TEvent : DomainEventBase<TAggregateId>
        {
            if (expression)
            {
                RaiseEvent(@event: @event);
            }
        }

        protected void RaiseEvent<TEvent>(TEvent @event)
            where TEvent : DomainEventBase<TAggregateId>
        {
            IDomainEvent<TAggregateId> eventWithAggregate = @event.WithAggregate(
                aggregateId: Equals(objA: Id, objB: default(TAggregateId)) ? @event.AggregateId : Id, aggregateVersion: _version);

            ((IEventSourcingAggregate<TAggregateId>)this).ApplyEvent(@event: eventWithAggregate, version: _version + 1);
            _uncommitedEvents.Add(item: eventWithAggregate);
        }
    }
}