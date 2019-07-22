using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Library.Domain.DomainEvents
{
    public class InHaushaltsbuchEingezahltEvent : DomainEventBase<HaushaltsbuchId>
    {
        public Buchungsbetrag Buchungsbetrag { get; private set; }
        public Buchungsdatum EinzahlungsDatum { get; private set; }

        private InHaushaltsbuchEingezahltEvent()
        {
        }

        internal InHaushaltsbuchEingezahltEvent(Buchungsbetrag betrag, Buchungsdatum einzahlungsDatum)
        {
            Buchungsbetrag = betrag;
            EinzahlungsDatum = einzahlungsDatum;
        }

        internal InHaushaltsbuchEingezahltEvent(
            HaushaltsbuchId aggregateId,
            long aggregateVersion,
            Buchungsbetrag betrag,
            Buchungsdatum einzahlungsDatum)
            : base(aggregateId: aggregateId, aggregateVersion: aggregateVersion)
        {
            Buchungsbetrag = betrag;
            EinzahlungsDatum = einzahlungsDatum;
        }

        public override IDomainEvent<HaushaltsbuchId> WithAggregate(HaushaltsbuchId aggregateId, long aggregateVersion) =>
            new InHaushaltsbuchEingezahltEvent(
                aggregateId: aggregateId,
                aggregateVersion: aggregateVersion,
                betrag: Buchungsbetrag,
                einzahlungsDatum: EinzahlungsDatum);
    }
}