using System.Collections.Generic;
using HaushaltsbuchAuszahlung = Haushaltsbuch.Library.Domain.ReadModel.HaushaltsbuchAuszahlung;
using HaushaltsbuchEinzahlung = Haushaltsbuch.Library.Domain.ReadModel.HaushaltsbuchEinzahlung;

namespace Haushaltsbuch.UI.Web.Models
{
    public class HaushaltsbuchIndexViewModel
    {
        public Library.Domain.ReadModel.Haushaltsbuch Haushaltsbuch { get; set; }

        public IEnumerable<HaushaltsbuchEinzahlung> Einzahlungen { get; set; }

        public IEnumerable<HaushaltsbuchAuszahlung> Auszahlungen { get; set; }
    }
}