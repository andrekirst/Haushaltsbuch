using System.Threading;
using System.Threading.Tasks;

namespace Haushaltsbuch.UI.Web.Services.Benutzerkonto
{
    public interface IBenutzerkontoService
    {
        Task<bool> Registrieren(string anmeldenummer, string email, string passwortHash, string securityStamp);
        
        Task<WebApi.Benutzerkonto.Models.Benutzerkonto> SucheAnhandAnmeldenummer(
            string anmeldenummer,
            CancellationToken cancellationToken);
        
        Task<string> GenerateAnmeldenummer(CancellationToken cancellationToken);

        Task<bool> SetzeNormalisierteEMailAdresse(string anmeldenummer, string normalisierteEMail,
            CancellationToken cancellationToken);

        Task<string> LiefereSecurityStamp(string anmeldenummer, CancellationToken cancellationToken);

        Task<string> LiefereEMail(string anmeldenummer, CancellationToken cancellationToken);

        Task<WebApi.Benutzerkonto.Models.Benutzerkonto> SucheAnhandEMail(string email, CancellationToken cancellationToken);

        Task<string> LiefereUserName(string anmeldenummer, CancellationToken cancellationToken);

        Task<string> LiefereUserId(string anmeldenummer, CancellationToken cancellationToken);

        Task<string> LieferePasswordHash(string anmeldenummer, CancellationToken cancellationToken);

        Task Anmelden(string anmeldenummer, CancellationToken cancellationToken);
    }
}