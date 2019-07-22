using System.Collections.Generic;

namespace Haushaltsbuch.UI.Web.Models
{
    public class HomeIndexViewModel
    {
        public List<Library.Domain.ReadModel.Haushaltsbuch> Haushaltsbücher { get; set; } = new List<Library.Domain.ReadModel.Haushaltsbuch>();
    }
}