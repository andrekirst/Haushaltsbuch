using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Domain.Benutzerkonto.DomainEvents
{
    public class BenutzerkontoRegistriertEvent : DomainEventBase<BenutzerkontoId>
    {
        public string Anmeldenummer { get; private set; }
        public string EMail { get; private set; }
        public string PasswortHash { get; private set; }
        public string SecurityStamp { get; private set; }

        private  BenutzerkontoRegistriertEvent()
        {}

        internal BenutzerkontoRegistriertEvent(
            string anmeldenummer,
            string email,
            string passwortHash,
            string securityStamp,
            BenutzerkontoId benutzerkontoId)
            : base(aggregateId: benutzerkontoId)
        {
            Anmeldenummer = anmeldenummer;
            EMail = email;
            PasswortHash = passwortHash;
            SecurityStamp = securityStamp;
        }

        private BenutzerkontoRegistriertEvent(
            BenutzerkontoId benutzerkontoId,
            long aggregateVersion,
            string anmeldenummer,
            string email,
            string passwortHash,
            string securityStamp)
            : base(aggregateId: benutzerkontoId, aggregateVersion: aggregateVersion)
        {
            Anmeldenummer = anmeldenummer;
            EMail = email;
            PasswortHash = passwortHash;
            SecurityStamp = securityStamp;
        }

        public override IDomainEvent<BenutzerkontoId> WithAggregate(BenutzerkontoId aggregateId, long aggregateVersion) =>
            new BenutzerkontoRegistriertEvent(
                benutzerkontoId: aggregateId,
                aggregateVersion: aggregateVersion,
                anmeldenummer: Anmeldenummer,
                email: EMail,
                passwortHash: PasswortHash,
                securityStamp: SecurityStamp);
    }
}