using System;

namespace Haushaltsbuch.Library.Domain.ReadModel
{
    public class HaushaltsbuchAuszahlung : IReadEntity
    {
        public string Id { get; set; }

        public double Betrag { get; set; }

        public DateTimeOffset Auszahlungsdatum { get; set; }

        public Kategorie Kategorie { get; set; }

        public string Memotext { get; set; }

        public string HaushaltsbuchId { get; set; }

        public override string ToString() =>
            $"Id: {Id}, Betrag: {Betrag:0.00} : {Auszahlungsdatum} (Kategorie: {Kategorie.Name}, Memotext: {Memotext})";

        public static HaushaltsbuchAuszahlung CreateFor(string haushaltsbuchId) =>
            new HaushaltsbuchAuszahlung
            {
                Id = $"{Guid.NewGuid()}@{haushaltsbuchId}",
                HaushaltsbuchId = haushaltsbuchId
            };
    }
}