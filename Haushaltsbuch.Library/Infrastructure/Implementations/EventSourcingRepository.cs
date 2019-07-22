using System;
using System.Reflection;
using System.Threading.Tasks;
using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Library.Infrastructure.Implementations
{
    public class EventSourcingRepository<TAggregate, TAggregateId>
        : IRepository<TAggregate, TAggregateId>
        where TAggregate : AggregateBase<TAggregateId>, IAggregate<TAggregateId>
        where TAggregateId : IAggregateId
    {
        public IEventStore EventStore { get; }
        public ITransientDomainEventPublisher Publisher { get; }

        public EventSourcingRepository(
            IEventStore eventStore,
            ITransientDomainEventPublisher publisher)
        {
            EventStore = eventStore;
            Publisher = publisher;
        }

        public async Task<TAggregate> GetByIdAsync(TAggregateId id)
        {
            try
            {
                TAggregate aggregate = CreateEmptyAggregate();
                IEventSourcingAggregate<TAggregateId> aggregatePersistance = aggregate;

                foreach (Event<TAggregateId> @event in await EventStore.ReadEventsAsync(id: id))
                {
                    aggregatePersistance.ApplyEvent(@event: @event.DomainEvent, version: @event.EventNumber);
                }

                return aggregate;
            }
            catch (EventStoreAggregateNotFoundException)
            {
                return null;
            }
            catch (EventStoreCommunicationException ex)
            {
                throw new RepositoryException(message: "Unable to access persistance layer", innerException: ex);
            }
        }

        public async Task SaveAsync(TAggregate aggregate)
        {
            try
            {
                IEventSourcingAggregate<TAggregateId> aggregatePersistance = aggregate;
                foreach (IDomainEvent<TAggregateId> @event in aggregatePersistance.GetUncommittedEvents())
                {
                    await Task.WhenAll(
                        EventStore.AppendEventAsync(@event: @event),
                        Publisher.PublishAsync(publishedEvent: (dynamic)@event));
                }
            }
            catch (EventStoreCommunicationException ex)
            {
                throw new RepositoryException(message: "Unable to access persistance layer", innerException: ex);
            }
        }

        private static TAggregate CreateEmptyAggregate() =>
            (TAggregate)typeof(TAggregate)
                .GetConstructor(
                    bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                    binder: null,
                    types: new Type[0],
                    modifiers: new ParameterModifier[0])
                .Invoke(parameters: new object[0]);
    }
}