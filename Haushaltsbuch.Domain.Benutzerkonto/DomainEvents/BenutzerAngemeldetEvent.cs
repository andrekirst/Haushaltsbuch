using System;
using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Domain.Benutzerkonto.DomainEvents
{
    public class BenutzerAngemeldetEvent : DomainEventBase<BenutzerkontoId>
    {
        public string Anmeldenummer { get; private set; }
        public DateTimeOffset Anmeldedatum { get; private set; }
        
        private BenutzerAngemeldetEvent()
        {}

        internal BenutzerAngemeldetEvent(
            string anmeldenummer,
            DateTimeOffset anmeldedatum)
        {
            Anmeldenummer = anmeldenummer;
            Anmeldedatum = anmeldedatum;
        }

        internal BenutzerAngemeldetEvent(
            BenutzerkontoId benutzerkontoId,
            long aggregateVersion,
            string anmeldenummer,
            DateTimeOffset anmeldedatum)
            : base(aggregateId: benutzerkontoId, aggregateVersion: aggregateVersion)
        {
            Anmeldenummer = anmeldenummer;
            Anmeldedatum = anmeldedatum;
        }

        public override IDomainEvent<BenutzerkontoId> WithAggregate(BenutzerkontoId aggregateId, long aggregateVersion) =>
            new BenutzerAngemeldetEvent(
                benutzerkontoId: aggregateId,
                aggregateVersion: aggregateVersion,
                anmeldenummer: Anmeldenummer,
                anmeldedatum: Anmeldedatum);
    }
}