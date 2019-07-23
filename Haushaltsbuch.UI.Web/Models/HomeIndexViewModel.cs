using System.Collections.Generic;

namespace Haushaltsbuch.UI.Web.Models
{
    public class HomeIndexViewModel
    {
        public List<Domain.Haushaltsbuch.ReadModel.Haushaltsbuch> Haushaltsbücher { get; set; } = new List<Domain.Haushaltsbuch.ReadModel.Haushaltsbuch>();
    }
}