using System;

namespace Haushaltsbuch.Domain.Haushaltsbuch
{
    public class HaushaltsbuchAuszahlung
    {
        public Guid AuszahlungsId { get; }
        public Buchungsbetrag Betrag { get; }
        public Buchungsdatum Auszahlungsdatum { get; }
        public Kategorie Kategorie { get; }
        public string Memotext { get; }

        public HaushaltsbuchAuszahlung(Guid auszahlungsId, Buchungsbetrag betrag, Buchungsdatum auszahlungsdatum, Kategorie kategorie, string memotext)
        {
            AuszahlungsId = auszahlungsId;
            Betrag = betrag;
            Auszahlungsdatum = auszahlungsdatum;
            Kategorie = kategorie;
            Memotext = memotext;
        }

        public override bool Equals(object obj) =>
            obj is HaushaltsbuchAuszahlung other
            && Equals(objA: AuszahlungsId, objB: other.AuszahlungsId)
            && Equals(objA: Betrag, objB: other.Betrag)
            && Equals(objA: Auszahlungsdatum, objB: other.Auszahlungsdatum)
            && Equals(objA: Kategorie, objB: other.Kategorie)
            && Equals(objA: Memotext, objB: other.Memotext);

        public override int GetHashCode() =>
            HashCode.Combine(value1: AuszahlungsId, value2: Betrag, value3: Auszahlungsdatum, value4: Kategorie, value5: Memotext);

        public override string ToString() => $"{AuszahlungsId} => {Betrag:0.00} : {Auszahlungsdatum} (Kategorie: \"{Kategorie.Name}\" - Memotext: \"{Memotext}\")";
    }
}
