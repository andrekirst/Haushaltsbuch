using System;

namespace Haushaltsbuch.UI.Web.Areas.Haushaltsbuch.Models
{
    public class EinzahlungAddViewModel
    {
        public string HaushaltsbuchId { get; set; }

        public double Betrag { get; set; }

        public string WährungSymbol { get; set; }

        public string WährungName { get; set; }

        public DateTime? Einzahlungdatum { get; set; }

        public string HaushaltsbuchName { get; set; }
    }
}