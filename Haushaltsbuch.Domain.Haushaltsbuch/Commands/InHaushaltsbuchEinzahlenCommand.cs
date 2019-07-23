using System;
using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Domain.Haushaltsbuch.Commands
{
    public class InHaushaltsbuchEinzahlenCommand : Command
    {
        public double Betrag { get; set; }
        public DateTimeOffset? EinzahlungsDatum { get; set; }

        private InHaushaltsbuchEinzahlenCommand()
        {
        }

        public InHaushaltsbuchEinzahlenCommand(double betrag, DateTimeOffset? einzahlungsDatum)
        {
            Betrag = betrag;
            EinzahlungsDatum = einzahlungsDatum;
        }
    }
}