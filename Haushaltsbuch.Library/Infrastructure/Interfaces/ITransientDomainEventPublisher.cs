using System.Threading.Tasks;

namespace Haushaltsbuch.Library.Infrastructure.Interfaces
{
    public interface ITransientDomainEventPublisher
    {
        Task PublishAsync<T>(T publishedEvent);
    }
}