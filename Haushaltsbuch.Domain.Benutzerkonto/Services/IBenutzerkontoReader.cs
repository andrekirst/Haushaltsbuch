using System.Threading.Tasks;

namespace Haushaltsbuch.Domain.Benutzerkonto.Services
{
    public interface IBenutzerkontoReader
    {
        Task<ReadModel.Benutzerkonto> GetById(string id);
        Task<ReadModel.Benutzerkonto> GetByAnmeldenummer(string anmeldenummer);
        Task<ReadModel.Benutzerkonto> GetByEMail(string email);
    }
}