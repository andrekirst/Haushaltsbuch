using System.Collections.Generic;
using Haushaltsbuch.WebApi.Models.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Haushaltsbuch.UI.Web.Models
{
    public class HaushaltsbuchAddViewModel
    {
        public string Name { get; set; }

        public List<Währung> Währungen { get; set; }

        public List<SelectListItem> WährungenItems { get; set; }
        
        public string NeueWährung { get; set; }
    }
}