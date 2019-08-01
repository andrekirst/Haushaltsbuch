namespace Haushaltsbuch.Domain.Benutzerkonto
{
    public class Passwort
    {
        public string PasswortHash { get; private set; }
        public string SecurityStamp { get; private set; }

        public Passwort(string passwortHash)
        {
            PasswortHash = passwortHash;
        }

        public Passwort(string passwortHash, string securityStamp)
            : this(passwortHash: passwortHash)
        {
            SecurityStamp = securityStamp;
        }
    }
}