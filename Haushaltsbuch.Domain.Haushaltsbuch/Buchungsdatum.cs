using System;

namespace Haushaltsbuch.Domain.Haushaltsbuch
{
    public class Buchungsdatum
    {
        public DateTimeOffset Datum { get; private set; }

        private Buchungsdatum()
        {
        }

        public Buchungsdatum(DateTimeOffset datum)
        {
            Datum = datum;
        }

        public override bool Equals(object obj) =>
            obj is Buchungsdatum other
            && Equals(objA: Datum, objB: other.Datum);

        public override int GetHashCode() => Datum.GetHashCode();

        public override string ToString() => Datum.ToString();

        public static bool operator ==(Buchungsdatum left, Buchungsdatum right) =>
            left != null && left.Equals(obj: right);

        public static bool operator !=(Buchungsdatum left, Buchungsdatum right) =>
            !(left == right);

        public static implicit operator DateTimeOffset(Buchungsdatum datum) => datum.Datum;
    }
}