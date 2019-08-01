using System.Threading.Tasks;

namespace Haushaltsbuch.Library.Infrastructure.Interfaces
{
    public interface IDomainEventHandler<TAggregateId, TEvent>
        where TEvent : IDomainEvent<TAggregateId>
    {
        Task HandleAsync(TEvent @event);
    }
}