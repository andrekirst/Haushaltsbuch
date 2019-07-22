using System.Collections.Generic;

namespace Haushaltsbuch.UI.Web.Models
{
    public class HomeSystemViewModel
    {
        public string Hostname { get; set; }

        public Dictionary<string, string> EnvironmentVariables { get; set; }
    }
}