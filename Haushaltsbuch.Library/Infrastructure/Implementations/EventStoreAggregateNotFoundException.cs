using System;
using System.Runtime.Serialization;

namespace Haushaltsbuch.Library.Infrastructure.Implementations
{
    [Serializable]
    internal class EventStoreAggregateNotFoundException : Exception
    {
        public EventStoreAggregateNotFoundException()
        {
        }

        public EventStoreAggregateNotFoundException(string message) : base(message: message)
        {
        }

        public EventStoreAggregateNotFoundException(string message, Exception innerException) : base(message: message, innerException: innerException)
        {
        }

        protected EventStoreAggregateNotFoundException(SerializationInfo info, StreamingContext context) : base(info: info, context: context)
        {
        }
    }
}