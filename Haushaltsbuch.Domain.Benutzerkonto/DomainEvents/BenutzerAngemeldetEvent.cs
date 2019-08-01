using System;
using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Domain.Benutzerkonto.DomainEvents
{
    public class BenutzerAngemeldetEvent : DomainEventBase<BenutzerkontoId>
    {
        public string Anmeldenummer { get; private set; }
        public DateTime Anmeldedatum { get; private set; }
        
        private BenutzerAngemeldetEvent()
        {}

        internal BenutzerAngemeldetEvent(
            string anmeldenummer,
            DateTime anmeldedatum)
        {
            Anmeldenummer = anmeldenummer;
            Anmeldedatum = anmeldedatum;
        }

        internal BenutzerAngemeldetEvent(
            BenutzerkontoId benutzerkontoId,
            long aggregateVersion,
            string anmeldenummer,
            DateTime anmeldedatum)
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