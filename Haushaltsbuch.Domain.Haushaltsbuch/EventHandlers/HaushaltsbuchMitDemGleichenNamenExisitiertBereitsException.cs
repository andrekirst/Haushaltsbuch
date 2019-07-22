using System;
using System.Runtime.Serialization;

namespace Haushaltsbuch.Library.Domain.EventHandlers
{
    [Serializable]
    internal class HaushaltsbuchMitDemGleichenNamenExisitiertBereitsException : Exception
    {
        public HaushaltsbuchMitDemGleichenNamenExisitiertBereitsException()
        {
        }

        public HaushaltsbuchMitDemGleichenNamenExisitiertBereitsException(string message) : base(message: message)
        {
        }

        public HaushaltsbuchMitDemGleichenNamenExisitiertBereitsException(string message, Exception innerException) : base(message: message, innerException: innerException)
        {
        }

        protected HaushaltsbuchMitDemGleichenNamenExisitiertBereitsException(SerializationInfo info, StreamingContext context) : base(info: info, context: context)
        {
        }
    }
}