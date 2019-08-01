using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Domain.Benutzerkonto.ReadModel
{
    public class Benutzerkonto : IReadEntity
    {
        public string Id { get; set; }

        public string Anmeldenummer { get; set; }
        
        public string PasswortHash { get; set; }
        
        public string EMail { get; set; }
        
        public string SecurityStamp { get; set; }
        
        public string NormalisierteEMail { get; set; }
    }
}