namespace Haushaltsbuch.Domain.Haushaltsbuch
{
    public class Buchungsbetrag
    {
        public double Betrag { get; private set; }
        public Währung Währung { get; private set; }

        private Buchungsbetrag()
        {
        }

        public Buchungsbetrag(double betrag, Währung währung)
        {
            Betrag = betrag;
            Währung = währung;
        }

        public override int GetHashCode() => Betrag.GetHashCode();

        public override bool Equals(object obj) =>
            obj is Buchungsbetrag other
            && Equals(objA: Betrag, objB: other.Betrag);

        public override string ToString() => $"{Betrag:0.00} {Währung.Symbol ?? "?"}";

        public static implicit operator double(Buchungsbetrag buchungsbetrag) => buchungsbetrag.Betrag;
    }
}