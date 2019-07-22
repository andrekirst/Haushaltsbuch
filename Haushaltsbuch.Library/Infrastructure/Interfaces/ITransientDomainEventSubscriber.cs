using System;
using System.Threading.Tasks;

namespace Haushaltsbuch.Library.Infrastructure.Interfaces
{
    public interface ITransientDomainEventSubscriber
    {
        void Subscribe<T>(Action<T> handler);
        void Subscribe<T>(Func<T, Task> handler);
    }
}