using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haushaltsbuch.Domain.Benutzerkonto.DomainEvents;
using Haushaltsbuch.Library.Infrastructure.Interfaces;
using Haushaltsbuch.Library.Infrastructure.Interfaces.Persistance;

namespace Haushaltsbuch.Domain.Benutzerkonto.EventHandlers
{
    public class BenutzerkontoEventHandler :
            IDomainEventHandler<BenutzerkontoId, BenutzerkontoRegistriertEvent>,
            IDomainEventHandler<BenutzerkontoId, EMailAdresseNormalisiertEvent>
    {
        private IRepository<ReadModel.Benutzerkonto> BenutzerkontoRepository { get; }

        public BenutzerkontoEventHandler(
            IRepository<ReadModel.Benutzerkonto> benutzerkontoRepository)
        {
            BenutzerkontoRepository = benutzerkontoRepository;
        }

        public async Task HandleAsync(BenutzerkontoRegistriertEvent @event)
        {
            IEnumerable<ReadModel.Benutzerkonto> benutzerkonten = await BenutzerkontoRepository.FindAllAsync(predicate: p => p.Anmeldenummer == @event.Anmeldenummer);
            if (benutzerkonten.Any())
            {
                throw new AnmeldenummerBereitsVergebenException(
                    message: $"Die Anmeldenummer {@event.Anmeldenummer} ist bereits vergeben",
                    anmeldenummer: @event.Anmeldenummer);
            }

            await BenutzerkontoRepository.InsertAsync(entity: new ReadModel.Benutzerkonto
            {
                Id = @event.AggregateId.Identifier,
                Anmeldenummer = @event.Anmeldenummer,
                EMail = @event.EMail,
                PasswortHash = @event.PasswortHash,
                SecurityStamp = @event.SecurityStamp
            });
        }

        public async Task HandleAsync(EMailAdresseNormalisiertEvent @event)
        {
            ReadModel.Benutzerkonto benutzerkonto =
                (await BenutzerkontoRepository.FindAllAsync(predicate: p => p.Anmeldenummer == @event.Anmeldenummer))
                .FirstOrDefault();

            if (benutzerkonto == null)
            {
                throw new BenutzerkontoAnhandAnmeldenummerNichtGefundenException(
                    message: $"Es wurde kein Benutzerkonto für die Anmeldenummer {@event.Anmeldenummer} gefunden werden",
                    anmeldenummer: @event.Anmeldenummer);
            }

            benutzerkonto.NormalisierteEMail = @event.NormalisierteEMailAdresse;
            await BenutzerkontoRepository.UpdateAsync(entity: benutzerkonto);
        }
    }
}