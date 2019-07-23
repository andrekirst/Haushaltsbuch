using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Haushaltsbuch.Domain.Haushaltsbuch.DomainEvents;
using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Domain.Haushaltsbuch.Services
{
    public class HaushaltsbuchWriter : IHaushaltsbuchWriter
    {
        public IRepository<Haushaltsbuch, HaushaltsbuchId> HaushaltsbuchRepository { get; }
        public ITransientDomainEventSubscriber Subscriber { get; }
        public IEnumerable<IDomainEventHandler<HaushaltsbuchId, HaushaltsbuchErstelltEvent>> HaushaltsbuchErstelltEventHandlers { get; }
        public IEnumerable<IDomainEventHandler<HaushaltsbuchId, InHaushaltsbuchEingezahltEvent>> InHaushaltsbuchEingezahltEventHandlers { get; }
        public IEnumerable<IDomainEventHandler<HaushaltsbuchId, AusHaltshaltsbuchAusgezahltEvent>> AusHaushaltsbuchAusgezahltEventHandlers { get; }
        private IEnumerable<IDomainEventHandler<HaushaltsbuchId, HaushaltsbuchUmbenanntEvent>> HaushaltsbuchUmbenanntEventHandlers { get; }

        public HaushaltsbuchWriter(
            IRepository<Haushaltsbuch, HaushaltsbuchId> haushaltsbuchRepository,
            ITransientDomainEventSubscriber subscriber,
            IEnumerable<IDomainEventHandler<HaushaltsbuchId, HaushaltsbuchErstelltEvent>> haushaltsbuchErstelltEventHandlers,
            IEnumerable<IDomainEventHandler<HaushaltsbuchId, InHaushaltsbuchEingezahltEvent>> inHaushaltsbuchEingezahltEventHandlers,
            IEnumerable<IDomainEventHandler<HaushaltsbuchId, AusHaltshaltsbuchAusgezahltEvent>> ausHaushaltsbuchAusgezahltEventHandlers,
            IEnumerable<IDomainEventHandler<HaushaltsbuchId, HaushaltsbuchUmbenanntEvent>> haushaltsbuchUmbenanntEventHandlers)
        {
            HaushaltsbuchRepository = haushaltsbuchRepository;
            Subscriber = subscriber;
            HaushaltsbuchErstelltEventHandlers = haushaltsbuchErstelltEventHandlers;
            InHaushaltsbuchEingezahltEventHandlers = inHaushaltsbuchEingezahltEventHandlers;
            AusHaushaltsbuchAusgezahltEventHandlers = ausHaushaltsbuchAusgezahltEventHandlers;
            HaushaltsbuchUmbenanntEventHandlers = haushaltsbuchUmbenanntEventHandlers;
        }

        public async Task ErstellenAsync(string name, Währung währung)
        {
            Haushaltsbuch haushaltsbuch = new Haushaltsbuch(haushaltsbuchId: HaushaltsbuchId.NeueHaushaltsbuchId(), name: name, währung: währung);
            Subscriber.Subscribe<HaushaltsbuchErstelltEvent>(handler: async @event => await HandleAsync(handlers: HaushaltsbuchErstelltEventHandlers, @event: @event));
            await HaushaltsbuchRepository.SaveAsync(aggregate: haushaltsbuch);
        }

        public async Task EinzahlenAsync(string haushaltsbuchId, double betrag, DateTimeOffset? einzahlungsdatum)
        {
            Haushaltsbuch haushaltsbuch = await HaushaltsbuchRepository.GetByIdAsync(id: new HaushaltsbuchId(id: haushaltsbuchId));
            Subscriber.Subscribe<InHaushaltsbuchEingezahltEvent>(handler: async @event => await HandleAsync(handlers: InHaushaltsbuchEingezahltEventHandlers, @event: @event));
            haushaltsbuch.Einzahlen(betrag: betrag, einzahlungsDatum: einzahlungsdatum);
            await HaushaltsbuchRepository.SaveAsync(aggregate: haushaltsbuch);
        }

        public async Task AuszahlenAsync(string haushaltsbuchId, double betrag, DateTimeOffset? auszahlungsdatum, Kategorie kategorie, string memotext)
        {
            Haushaltsbuch haushaltsbuch = await HaushaltsbuchRepository.GetByIdAsync(id: new HaushaltsbuchId(id: haushaltsbuchId));
            Subscriber.Subscribe<AusHaltshaltsbuchAusgezahltEvent>(handler: async @event => await HandleAsync(handlers: AusHaushaltsbuchAusgezahltEventHandlers, @event: @event));
            haushaltsbuch.Auszahlen(betrag: betrag, auszahlungsDatum: auszahlungsdatum, kategorie: kategorie, memotext: memotext);
            await HaushaltsbuchRepository.SaveAsync(aggregate: haushaltsbuch);
        }

        public async Task Umbenennen(string haushaltsbuchId, string neuerName)
        {
            Haushaltsbuch haushaltsbuch = await HaushaltsbuchRepository.GetByIdAsync(id: new HaushaltsbuchId(id: haushaltsbuchId));
            Subscriber.Subscribe<HaushaltsbuchUmbenanntEvent>(handler: async @event => await HandleAsync(handlers: HaushaltsbuchUmbenanntEventHandlers, @event: @event));
            haushaltsbuch.Umbenennen(neuerName: neuerName);
            await HaushaltsbuchRepository.SaveAsync(aggregate: haushaltsbuch);
        }

        public async Task HandleAsync<T>(IEnumerable<IDomainEventHandler<HaushaltsbuchId, T>> handlers, T @event)
            where T : IDomainEvent<HaushaltsbuchId>
        {
            foreach (IDomainEventHandler<HaushaltsbuchId, T> handler in handlers)
            {
                await handler.HandleAsync(@event: @event);
            }
        }
    }
}