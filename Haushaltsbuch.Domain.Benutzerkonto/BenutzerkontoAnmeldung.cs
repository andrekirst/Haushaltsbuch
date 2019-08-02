using System;

namespace Haushaltsbuch.Domain.Benutzerkonto
{
    public class BenutzerkontoAnmeldung
    {
        public Guid Id { get; }
        public string Anmeldenummer { get; }
        public DateTimeOffset Anmeldedatum { get; }

        public BenutzerkontoAnmeldung(Guid id, string anmeldenummer, DateTimeOffset anmeldedatum)
        {
            Id = id;
            Anmeldenummer = anmeldenummer;
            Anmeldedatum = anmeldedatum;
        }

        public override bool Equals(object obj) =>
            obj is BenutzerkontoAnmeldung other &&
            Equals(objA: Id, objB: other.Id) &&
            Equals(objA: Anmeldenummer, objB: other.Anmeldenummer) &&
            Equals(objA: Anmeldedatum, objB: other.Anmeldedatum);

        public override int GetHashCode() =>
            HashCode.Combine(value1: Id, value2: Anmeldenummer, value3: Anmeldedatum);

        public override string ToString() =>
            $"{Anmeldenummer}: {Anmeldedatum:dd.MM.yyyy hh:mm:ss}";
    }
}