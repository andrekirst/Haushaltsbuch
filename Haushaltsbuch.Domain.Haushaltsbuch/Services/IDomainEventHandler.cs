using System.Threading.Tasks;
using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Domain.Haushaltsbuch.Services
{
    public interface IDomainEventHandler<TAggregateId, TEvent>
        where TEvent : IDomainEvent<TAggregateId>
    {
        Task HandleAsync(TEvent @event);
    }
}