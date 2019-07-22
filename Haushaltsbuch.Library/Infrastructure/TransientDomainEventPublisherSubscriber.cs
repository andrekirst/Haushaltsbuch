using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Library.Infrastructure
{
    public class TransientDomainEventPublisherSubscriber : IDisposable, ITransientDomainEventSubscriber, ITransientDomainEventPublisher
    {
        private static AsyncLocal<Dictionary<Type, List<object>>> handlers = new AsyncLocal<Dictionary<Type, List<object>>>();

        public Dictionary<Type, List<object>> Handlers => handlers.Value ?? (handlers.Value = new Dictionary<Type, List<object>>());

        public TransientDomainEventPublisherSubscriber()
        {
        }

        public void Dispose()
        {
            foreach (var handlersValue in Handlers.Values)
            {
                handlersValue.Clear();
            }
            Handlers.Clear();
        }

        public void Subscribe<T>(Action<T> handler)
        {
            GetHandlersOf<T>().Add(item: handler);
        }

        public void Subscribe<T>(Func<T, Task> handler)
        {
            GetHandlersOf<T>().Add(item: handler);
        }

        public async Task PublishAsync<T>(T publishedEvent)
        {
            foreach (object handler in GetHandlersOf<T>())
            {
                try
                {
                    switch (handler)
                    {
                        case Action<T> action:
                            action(obj: publishedEvent);
                            break;
                        case Func<T, Task> action:
                            await action(arg: publishedEvent);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(value: e);
                    throw;
                }
            }
        }

        private ICollection<object> GetHandlersOf<T>()
        {
            return Handlers.GetValueOrDefault(key: typeof(T)) ?? (Handlers[key: typeof(T)] = new List<object>());
        }
    }
}