using System;
using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Domain.Haushaltsbuch.Commands
{
    public class AusHaltshaltsbuchAuszahlenCommand : Command
    {
        public double Betrag { get; set; }
        public string Kategorie { get; set; }
        public string Memotext { get; set; }
        public DateTimeOffset? Auszahlungsdatum { get; set; }

        private AusHaltshaltsbuchAuszahlenCommand()
        {}

        public AusHaltshaltsbuchAuszahlenCommand(double betrag, string kategorie, string memotext, DateTimeOffset? auszahlungsdatum)
        {
            Betrag = betrag;
            Kategorie = kategorie;
            Memotext = memotext;
            Auszahlungsdatum = auszahlungsdatum;
        }
    }
}