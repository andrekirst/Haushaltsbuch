using System;
using System.Runtime.Serialization;

namespace Haushaltsbuch.Domain.Benutzerkonto.EventHandlers
{
    [Serializable]
    internal class AnmeldenummerBereitsVergebenException : Exception
    {
        public string Anmeldenummer { get; private set; }

        public AnmeldenummerBereitsVergebenException()
        {
        }

        public AnmeldenummerBereitsVergebenException(string message) : base(message)
        {
        }

        public AnmeldenummerBereitsVergebenException(string message, string anmeldenummer)
            : base(message: message)
        {
            Anmeldenummer = anmeldenummer;
        }

        public AnmeldenummerBereitsVergebenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AnmeldenummerBereitsVergebenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}