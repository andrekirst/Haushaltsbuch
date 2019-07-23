using System.Collections.Generic;
using HaushaltsbuchAuszahlung = Haushaltsbuch.Domain.Haushaltsbuch.ReadModel.HaushaltsbuchAuszahlung;
using HaushaltsbuchEinzahlung = Haushaltsbuch.Domain.Haushaltsbuch.ReadModel.HaushaltsbuchEinzahlung;

namespace Haushaltsbuch.UI.Web.Areas.Haushaltsbuch.Models
{
    public class HaushaltsbuchIndexViewModel
    {
        public Domain.Haushaltsbuch.ReadModel.Haushaltsbuch Haushaltsbuch { get; set; }

        public IEnumerable<HaushaltsbuchEinzahlung> Einzahlungen { get; set; }

        public IEnumerable<HaushaltsbuchAuszahlung> Auszahlungen { get; set; }
    }
}