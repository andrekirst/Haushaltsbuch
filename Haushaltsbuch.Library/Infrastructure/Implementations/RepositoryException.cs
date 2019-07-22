using System;
using System.Runtime.Serialization;

namespace Haushaltsbuch.Library.Infrastructure.Implementations
{
    [Serializable]
    public class RepositoryException : Exception
    {
        public RepositoryException()
        {
        }

        public RepositoryException(string message) : base(message: message)
        {
        }

        public RepositoryException(string message, Exception innerException) : base(message: message, innerException: innerException)
        {
        }

        protected RepositoryException(SerializationInfo info, StreamingContext context) : base(info: info, context: context)
        {
        }
    }
}