using System;

namespace Haushaltsbuch.WebApi.Haushaltsbuch.Models.Dto.Commands
{
    public class AusHaltshaltsbuchAuszahlenCommand
    {
        public double Betrag { get; set; }
        public DateTimeOffset? Auszahlungsdatum { get; set; }
        public string Kategorie { get; set; }
        public string Memotext { get; set; }

        public AusHaltshaltsbuchAuszahlenCommand()
        {
        }

        public AusHaltshaltsbuchAuszahlenCommand(double betrag, DateTimeOffset? auszahlungsdatum, string kategorie, string memotext)
        {
            Betrag = betrag;
            Auszahlungsdatum = auszahlungsdatum;
            Kategorie = kategorie;
            Memotext = memotext;
        }
    }
}