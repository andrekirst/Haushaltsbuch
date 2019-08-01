using System.Threading.Tasks;

namespace Haushaltsbuch.Domain.Benutzerkonto.Services
{
    public interface IBenutzerkontoWriter
    {
        Task Registrieren(string anmeldenummer, string email, string passwortHash, string securityStamp);

        Task SetzeNormalisierteEMailAdresse(string benutzerkontoId, string anmeldenummer,
            string normalisierteEMailAdresse);
    }
}