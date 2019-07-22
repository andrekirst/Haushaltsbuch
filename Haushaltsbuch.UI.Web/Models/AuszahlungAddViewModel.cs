using System;

namespace Haushaltsbuch.UI.Web.Models
{
    public class AuszahlungAddViewModel
    {
        public double Betrag { get; set; }

        public DateTimeOffset? Auszahlungsdatum { get; set; }

        public string Kategorie { get; set; }

        public string Memotext { get; set; }
        
        public string HaushaltsbuchId { get; set; }
        
        public string HaushaltsbuchName { get; set; }
        
        public string WährungSymbol { get; set; }

        public string WährungName { get; set; }
    }
}