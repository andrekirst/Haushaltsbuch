using System;
using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Library.Domain.Commands
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