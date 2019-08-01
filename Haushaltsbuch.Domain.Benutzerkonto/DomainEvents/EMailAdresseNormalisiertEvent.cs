using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Domain.Benutzerkonto.DomainEvents
{
    public class EMailAdresseNormalisiertEvent : DomainEventBase<BenutzerkontoId>
    {
        public string Anmeldenummer { get; private set; }
        public string NormalisierteEMailAdresse { get; private set; }

        private EMailAdresseNormalisiertEvent()
        {}

        internal EMailAdresseNormalisiertEvent(string anmeldenummer, string normalisierteEMailAdresse)
        {
            Anmeldenummer = anmeldenummer;
            NormalisierteEMailAdresse = normalisierteEMailAdresse;
        }

        internal EMailAdresseNormalisiertEvent(
            BenutzerkontoId benutzerkontoId,
            long aggregateVersion,
            string anmeldenummer,
            string normalisierteEMailAdresse)
            : base(aggregateId: benutzerkontoId, aggregateVersion: aggregateVersion)
        {
            Anmeldenummer = anmeldenummer;
            NormalisierteEMailAdresse = normalisierteEMailAdresse;
        }

        public override IDomainEvent<BenutzerkontoId> WithAggregate(BenutzerkontoId aggregateId, long aggregateVersion)
            => new EMailAdresseNormalisiertEvent(
                benutzerkontoId: aggregateId,
                aggregateVersion: aggregateVersion,
                anmeldenummer: Anmeldenummer,
                normalisierteEMailAdresse: NormalisierteEMailAdresse);
    }
}