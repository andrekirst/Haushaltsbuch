using System;

namespace Haushaltsbuch.WebApi.Models.Dto.Commands
{
    public class InHaushaltsbuchEinzahlenCommand
    {
        public double Betrag { get; set; }
        public DateTimeOffset? Einzahlungsdatum { get; set; }

        public InHaushaltsbuchEinzahlenCommand()
        {
        }

        public InHaushaltsbuchEinzahlenCommand(double betrag, DateTimeOffset? einzahlungsdatum)
        {
            Betrag = betrag;
            Einzahlungsdatum = einzahlungsdatum;
        }
    }
}