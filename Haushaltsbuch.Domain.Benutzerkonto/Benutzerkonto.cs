using EnsureThat;
using Haushaltsbuch.Domain.Benutzerkonto.DomainEvents;
using Haushaltsbuch.Library.Infrastructure.Extensions;
using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Domain.Benutzerkonto
{
    public class Benutzerkonto : AggregateBase<BenutzerkontoId>,
        IApplyEvent<BenutzerkontoRegistriertEvent>,
        IApplyEvent<EMailAdresseNormalisiertEvent>
    {
        public string Anmeldenummer { get; private set; }
        public EMailAdresse EMailAdresse { get; private set; }
        public Passwort Passwort { get; private set; }

        private Benutzerkonto()
        {
        }
        
        public Benutzerkonto(BenutzerkontoId benutzerkontoId, string anmeldenummer, string email, string passwortHash, string securityStamp)
        {
            RaiseEvent(@event: new BenutzerkontoRegistriertEvent(
                benutzerkontoId: benutzerkontoId,
                anmeldenummer: anmeldenummer,
                email: email,
                passwortHash: passwortHash,
                securityStamp: securityStamp));
        }

        public void SetzeNormalisierteEMail(string normalisiertEMail)
        {
            RaiseEvent(@event: new EMailAdresseNormalisiertEvent(
                anmeldenummer: Anmeldenummer,
                normalisierteEMailAdresse: normalisiertEMail));
        }

        public void ApplyEvent(BenutzerkontoRegistriertEvent @event)
        {
            Ensure.Bool.IsTrue(value: @event.EMail.IsValidEMailAddress(), paramName: nameof(@event.EMail));
            Id = @event.AggregateId;
            Anmeldenummer = @event.Anmeldenummer;
            EMailAdresse = new EMailAdresse(email: @event.EMail);
            Passwort = new Passwort(passwortHash: @event.PasswortHash, securityStamp: @event.SecurityStamp);
        }

        public void ApplyEvent(EMailAdresseNormalisiertEvent @event)
        {
            EMailAdresse = new EMailAdresse(
                email: EMailAdresse.EMail,
                normalizedEMail: @event.NormalisierteEMailAdresse);
        }
    }
}