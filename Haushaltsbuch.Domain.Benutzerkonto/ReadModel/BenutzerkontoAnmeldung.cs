using System;
using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Domain.Benutzerkonto.ReadModel
{
    public class BenutzerkontoAnmeldung : IReadEntity
    {
        public string Id { get; set; }

        public string Anmeldenummer { get; set; }

        public DateTimeOffset Anmeldedatum { get; set; }

        public string BenutzerkontoId { get; set; }

        public override string ToString() =>
            $"{Anmeldenummer}: {Anmeldedatum:dd.MM.yyyy hh:mm:ss}";

        public static BenutzerkontoAnmeldung CreateFor(string benutzerkontoId) =>
            new BenutzerkontoAnmeldung
            {
                Id = $"{Guid.NewGuid()}@{benutzerkontoId}",
                BenutzerkontoId = benutzerkontoId
            };
    }
}