using System;
using System.Runtime.Serialization;

namespace Haushaltsbuch.Library.Infrastructure.Implementations
{
    [Serializable]
    internal class EventStoreCommunicationException : Exception
    {
        public EventStoreCommunicationException()
        {
        }

        public EventStoreCommunicationException(string message) : base(message: message)
        {
        }

        public EventStoreCommunicationException(string message, Exception innerException) : base(message: message, innerException: innerException)
        {
        }

        protected EventStoreCommunicationException(SerializationInfo info, StreamingContext context) : base(info: info, context: context)
        {
        }
    }
}