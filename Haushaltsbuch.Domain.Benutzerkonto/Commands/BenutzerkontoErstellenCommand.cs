using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Domain.Benutzerkonto.Commands
{
    public class BenutzerkontoErstellenCommand : Command
    {
        public string Anmeldenummer { get; private set; }
        public string Email { get; private set; }

        public BenutzerkontoErstellenCommand(
            string anmeldenummer,
            string email)
        {
            Anmeldenummer = anmeldenummer;
            Email = email;
        }
    }
}