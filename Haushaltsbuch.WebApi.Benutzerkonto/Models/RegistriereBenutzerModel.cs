namespace Haushaltsbuch.WebApi.Benutzerkonto.Models
{
    public class RegistriereBenutzerModel
    {
        public string PasswortHash { get; set; }

        public string Anmeldenummer { get; set; }

        public string EMail { get; set; }

        public string SecurityStamp { get; set; }
    }
}