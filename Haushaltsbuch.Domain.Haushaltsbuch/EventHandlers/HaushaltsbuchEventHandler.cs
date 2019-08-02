using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haushaltsbuch.Domain.Haushaltsbuch.DomainEvents;
using Haushaltsbuch.Library.Infrastructure.Interfaces;
using Haushaltsbuch.Library.Infrastructure.Interfaces.Persistance;

namespace Haushaltsbuch.Domain.Haushaltsbuch.EventHandlers
{
    public class HaushaltsbuchEventHandler :
        IDomainEventHandler<HaushaltsbuchId, HaushaltsbuchErstelltEvent>,
        IDomainEventHandler<HaushaltsbuchId, InHaushaltsbuchEingezahltEvent>,
        IDomainEventHandler<HaushaltsbuchId, AusHaltshaltsbuchAusgezahltEvent>,
        IDomainEventHandler<HaushaltsbuchId, HaushaltsbuchUmbenanntEvent>
    {
        private IRepository<ReadModel.Haushaltsbuch> HaushaltsbuchRepository { get; }
        private IRepository<ReadModel.HaushaltsbuchAuszahlung> HaushaltsbuchAuszahlungenRepository { get; }
        private IRepository<ReadModel.HaushaltsbuchEinzahlung> HaushaltsbuchEinzahlungenRepository { get; }

        public HaushaltsbuchEventHandler(
            IRepository<ReadModel.Haushaltsbuch> haushaltsbuchRepository,
            IRepository<ReadModel.HaushaltsbuchAuszahlung> haushaltsbuchAuszahlungenRepository,
            IRepository<ReadModel.HaushaltsbuchEinzahlung> haushaltsbuchEinzahlungenRepository)
        {
            HaushaltsbuchRepository = haushaltsbuchRepository;
            HaushaltsbuchAuszahlungenRepository = haushaltsbuchAuszahlungenRepository;
            HaushaltsbuchEinzahlungenRepository = haushaltsbuchEinzahlungenRepository;
        }

        public async Task HandleAsync(HaushaltsbuchErstelltEvent @event)
        {
            IEnumerable<ReadModel.Haushaltsbuch> haushaltsbuecher = await HaushaltsbuchRepository.FindAllAsync(predicate: p => p.Name == @event.Name);
            if (haushaltsbuecher.Any())
            {
                throw new HaushaltsbuchMitDemGleichenNamenExisitiertBereitsException(message: @event.Name);
            }

            await HaushaltsbuchRepository.InsertAsync(entity: new ReadModel.Haushaltsbuch
            {
                Id = @event.AggregateId.Identifier,
                Kassenbestand = 0.0D,
                Name = @event.Name,
                WährungSymbol = @event.Währung.Symbol,
                WährungName = @event.Währung.Name
            });
        }

        public async Task HandleAsync(InHaushaltsbuchEingezahltEvent @event)
        {
            ReadModel.Haushaltsbuch haushaltsbuch = await HaushaltsbuchRepository.GetByIdAsync(id: @event.AggregateId.Identifier);
            ReadModel.HaushaltsbuchEinzahlung einzahlung = ReadModel.HaushaltsbuchEinzahlung.CreateFor(haushaltsbuchId: @event.AggregateId.Identifier);

            haushaltsbuch.Kassenbestand += @event.Buchungsbetrag;
            einzahlung.Betrag = @event.Buchungsbetrag;
            einzahlung.Einzahlungsdatum = @event.EinzahlungsDatum;
            
            await Task.WhenAll(
                HaushaltsbuchRepository.UpdateAsync(entity: haushaltsbuch),
                HaushaltsbuchEinzahlungenRepository.InsertAsync(entity: einzahlung));
        }

        public async Task HandleAsync(AusHaltshaltsbuchAusgezahltEvent @event)
        {
            ReadModel.Haushaltsbuch haushaltsbuch = await HaushaltsbuchRepository.GetByIdAsync(id: @event.AggregateId.Identifier);
            ReadModel.HaushaltsbuchAuszahlung auszahlung = ReadModel.HaushaltsbuchAuszahlung.CreateFor(haushaltsbuchId: @event.AggregateId.Identifier);

            haushaltsbuch.Kassenbestand -= @event.Buchungsbetrag;
            auszahlung.Betrag = @event.Buchungsbetrag;
            auszahlung.Auszahlungsdatum = @event.AuszahlungsDatum;
            auszahlung.Kategorie = @event.Kategorie;
            auszahlung.Memotext = @event.Memotext;

            await HaushaltsbuchRepository.UpdateAsync(entity: haushaltsbuch);
            await HaushaltsbuchAuszahlungenRepository.InsertAsync(entity: auszahlung);
        }

        public async Task HandleAsync(HaushaltsbuchUmbenanntEvent @event)
        {
            ReadModel.Haushaltsbuch haushaltsbuch = await HaushaltsbuchRepository.GetByIdAsync(id: @event.AggregateId.Identifier);
            haushaltsbuch.Name = @event.NeuerName;

            await HaushaltsbuchRepository.UpdateAsync(entity: haushaltsbuch);
        }
    }
}