using System.Threading;
using System.Threading.Tasks;

namespace Haushaltsbuch.UI.Web.Services.Benutzerkonto
{
    public interface IBenutzerkontoService
    {
        Task<WebApi.Benutzerkonto.Models.Benutzerkonto> FindByNameAsync(
            string anmeldenummer,
            CancellationToken cancellationToken);
    }
}