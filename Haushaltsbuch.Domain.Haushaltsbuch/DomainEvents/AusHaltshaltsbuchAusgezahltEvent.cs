using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Library.Domain.DomainEvents
{
    public class AusHaltshaltsbuchAusgezahltEvent : DomainEventBase<HaushaltsbuchId>
    {
        public Buchungsbetrag Buchungsbetrag { get; private set; }
        public Buchungsdatum AuszahlungsDatum { get; private set; }
        public Kategorie Kategorie { get; private set; }
        public string Memotext { get; private set; }

        private AusHaltshaltsbuchAusgezahltEvent()
        {
        }

        internal AusHaltshaltsbuchAusgezahltEvent(Buchungsbetrag betrag, Buchungsdatum auszahlungsDatum, Kategorie kategorie, string memotext)
        {
            Buchungsbetrag = betrag;
            AuszahlungsDatum = auszahlungsDatum;
            Kategorie = kategorie;
            Memotext = memotext;
        }

        internal AusHaltshaltsbuchAusgezahltEvent(HaushaltsbuchId aggregateId, long aggregateVersion, Buchungsbetrag betrag, Buchungsdatum auszahlungsDatum,
            Kategorie kategorie, string memotext)
            : base(aggregateId: aggregateId, aggregateVersion: aggregateVersion)
        {
            Buchungsbetrag = betrag;
            AuszahlungsDatum = auszahlungsDatum;
            Kategorie = kategorie;
            Memotext = memotext;
        }

        public override IDomainEvent<HaushaltsbuchId> WithAggregate(HaushaltsbuchId aggregateId, long aggregateVersion) =>
            new AusHaltshaltsbuchAusgezahltEvent(
                aggregateId: aggregateId,
                aggregateVersion: aggregateVersion,
                betrag: Buchungsbetrag,
                auszahlungsDatum: AuszahlungsDatum,
                kategorie: Kategorie,
                memotext: Memotext);
    }
}