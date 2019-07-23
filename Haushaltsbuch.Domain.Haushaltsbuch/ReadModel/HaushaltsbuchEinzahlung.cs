using System;

namespace Haushaltsbuch.Domain.Haushaltsbuch.ReadModel
{
    public class HaushaltsbuchEinzahlung : IReadEntity
    {
        public string Id { get; set; }

        public string HaushaltsbuchId { get; set; }

        public double Betrag { get; set; }

        public DateTimeOffset Einzahlungsdatum { get; set; }

        public override string ToString() =>
            $"Id: {Id}, {Betrag:0.00} : {Einzahlungsdatum}";

        public static HaushaltsbuchEinzahlung CreateFor(string haushaltsbuchId) =>
            new HaushaltsbuchEinzahlung
            {
                Id = $"{Guid.NewGuid()}@{haushaltsbuchId}",
                HaushaltsbuchId = haushaltsbuchId
            };
    }
}