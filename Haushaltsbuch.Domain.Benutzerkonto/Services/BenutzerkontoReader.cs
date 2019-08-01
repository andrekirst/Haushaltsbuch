using System.Linq;
using System.Threading.Tasks;
using Haushaltsbuch.Library.Infrastructure.Interfaces.Persistance;

namespace Haushaltsbuch.Domain.Benutzerkonto.Services
{
    public class BenutzerkontoReader : IBenutzerkontoReader
    {
        private IReadOnlyRepository<ReadModel.Benutzerkonto> BenutzerkontoRepository { get; }

        public BenutzerkontoReader(
            IReadOnlyRepository<ReadModel.Benutzerkonto> benutzerkontoRepository)
        {
            BenutzerkontoRepository = benutzerkontoRepository;
        }

        public Task<ReadModel.Benutzerkonto> GetById(string id) =>
            BenutzerkontoRepository.GetByIdAsync(id: id);

        public async Task<ReadModel.Benutzerkonto> GetByAnmeldenummer(string anmeldenummer) =>
            (await BenutzerkontoRepository.FindAllAsync(predicate: benutzerkonto => benutzerkonto.Anmeldenummer == anmeldenummer))
            .FirstOrDefault();

        public async Task<ReadModel.Benutzerkonto> GetByEMail(string email) =>
            (await BenutzerkontoRepository.FindAllAsync(predicate: benutzerkonto => benutzerkonto.EMail == email))
            .FirstOrDefault();
    }
}