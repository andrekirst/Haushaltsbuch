using System.Threading.Tasks;
using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Library.Domain.Services
{
    public interface IDomainEventHandler<TAggregateId, TEvent>
        where TEvent : IDomainEvent<TAggregateId>
    {
        Task HandleAsync(TEvent @event);
    }
}