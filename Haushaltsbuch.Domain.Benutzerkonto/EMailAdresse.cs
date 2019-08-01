namespace Haushaltsbuch.Domain.Benutzerkonto
{
    public class EMailAdresse
    {
        public string EMail { get; private set; }

        public string NormalizedEMail { get; private set; }

        public EMailAdresse(string email)
        {
            EMail = email;
        }

        public EMailAdresse(string email, string normalizedEMail)
            : this(email: email)
        {
            NormalizedEMail = normalizedEMail;
        }
    }
}