using System;
    
namespace Haushaltsbuch.Library.Domain
{
    public class HaushaltsbuchEinzahlung
    {
        public Guid EinzahlungsId { get; }
        public Buchungsbetrag Betrag { get; }
        public Buchungsdatum Einzahlungsdatum { get; }

        public HaushaltsbuchEinzahlung(Guid einzahlungsId, Buchungsbetrag betrag, Buchungsdatum einzahlungsdatum)
        {
            EinzahlungsId = einzahlungsId;
            Betrag = betrag;
            Einzahlungsdatum = einzahlungsdatum;
        }

        public override bool Equals(object obj) =>
            obj is HaushaltsbuchEinzahlung other
            && Equals(objA: other.EinzahlungsId, objB: EinzahlungsId)
            && Equals(objA: other.Betrag, objB: Betrag)
            && Equals(objA: other.Einzahlungsdatum, objB: Einzahlungsdatum);

        public override int GetHashCode() => HashCode.Combine(value1: EinzahlungsId, value2: Betrag, value3: Einzahlungsdatum);

        public override string ToString() => $"{EinzahlungsId} => {Betrag:0.00} : {Einzahlungsdatum}";
    }
}