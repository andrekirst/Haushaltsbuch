using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Haushaltsbuch.UI.Web.Areas.Benutzerkonto.Models
{
    public class BenutzerkontoIndexViewModel
    {
        public IEnumerable<IdentityError> Errors { get; set; }
    }
}