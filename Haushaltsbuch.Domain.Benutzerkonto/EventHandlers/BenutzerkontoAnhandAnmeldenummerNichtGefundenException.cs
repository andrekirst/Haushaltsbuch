using System;
using System.Runtime.Serialization;

namespace Haushaltsbuch.Domain.Benutzerkonto.EventHandlers
{
    [Serializable]
    internal class BenutzerkontoAnhandAnmeldenummerNichtGefundenException : Exception
    {
        public string Anmeldenummer { get; set; }

        public BenutzerkontoAnhandAnmeldenummerNichtGefundenException()
        {
        }

        public BenutzerkontoAnhandAnmeldenummerNichtGefundenException(string message)
            : base(message: message)
        {
        }

        public BenutzerkontoAnhandAnmeldenummerNichtGefundenException(string message, string anmeldenummer)
            : base(message: message)
        {
            Anmeldenummer = anmeldenummer;
        }

        public BenutzerkontoAnhandAnmeldenummerNichtGefundenException(string message, Exception innerException) : base(message: message, innerException: innerException)
        {
        }

        protected BenutzerkontoAnhandAnmeldenummerNichtGefundenException(SerializationInfo info, StreamingContext context) : base(info: info, context: context)
        {
        }
    }
}