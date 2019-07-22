using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haushaltsbuch.Library.Domain.ReadModel.Persistance;

namespace Haushaltsbuch.Library.Domain.Services
{
    public class HaushaltsbuchReader : IHaushaltsbuchReader
    {
        private IReadOnlyRepository<ReadModel.Haushaltsbuch> HaushaltsbuchRepository { get; }
        private IReadOnlyRepository<ReadModel.HaushaltsbuchAuszahlung> HaushaltsbuchAuszahlungRepository { get; }
        private IReadOnlyRepository<ReadModel.HaushaltsbuchEinzahlung> HaushaltsbucheinzahlungRepository { get; }

        public HaushaltsbuchReader(
            IReadOnlyRepository<ReadModel.Haushaltsbuch> haushaltsbuchRepository,
            IReadOnlyRepository<ReadModel.HaushaltsbuchAuszahlung> haushaltsbuchAuszahlungRepository,
            IReadOnlyRepository<ReadModel.HaushaltsbuchEinzahlung> haushaltsbucheinzahlungRepository)
        {
            HaushaltsbuchRepository = haushaltsbuchRepository;
            HaushaltsbuchAuszahlungRepository = haushaltsbuchAuszahlungRepository;
            HaushaltsbucheinzahlungRepository = haushaltsbucheinzahlungRepository;
        }

        public Task<IEnumerable<ReadModel.Haushaltsbuch>> GetAllAsync() =>
            HaushaltsbuchRepository.FindAllAsync(predicate: p => true);

        public Task<ReadModel.Haushaltsbuch> GetByIdAsync(string id) =>
            HaushaltsbuchRepository.GetByIdAsync(id: id);

        public async Task<ReadModel.Haushaltsbuch> GetByName(string name) =>
            (await HaushaltsbuchRepository.FindAllAsync(predicate: p => p.Name == name))
            .FirstOrDefault();

        public async Task<ReadModel.Haushaltsbuch> GetDefault() =>
            (await HaushaltsbuchRepository.FindAllAsync(predicate: p => true))
            .FirstOrDefault();

        public Task<IEnumerable<ReadModel.HaushaltsbuchAuszahlung>> GetAuszahlungenOfAsync(string haushaltsbuchId) =>
            HaushaltsbuchAuszahlungRepository.FindAllAsync(predicate: auszahlung => auszahlung.HaushaltsbuchId == haushaltsbuchId);

        public Task<IEnumerable<ReadModel.HaushaltsbuchEinzahlung>> GetEinzahlungenOfAsync(string haushaltsbuchId) =>
            HaushaltsbucheinzahlungRepository.FindAllAsync(predicate: einzahlung => einzahlung.HaushaltsbuchId == haushaltsbuchId);

        private List<Währung> _währungen = new List<Währung>
        {
            new Währung("$", "US-Dollar"),
            new Währung("€", "EUR")
        };

        public async Task<IEnumerable<Währung>> GetAllWährungen()
        {
            return _währungen;
        }

        public async Task<Währung> GetWährungByName(string name)
        {
            return _währungen.FirstOrDefault(währung => währung.Name == name);
        }
    }
}