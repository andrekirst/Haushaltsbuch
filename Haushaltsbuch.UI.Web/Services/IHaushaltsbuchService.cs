using System.Collections.Generic;
using System.Threading.Tasks;
using HaushaltsbuchAuszahlung = Haushaltsbuch.Library.Domain.ReadModel.HaushaltsbuchAuszahlung;
using HaushaltsbuchEinzahlung = Haushaltsbuch.Library.Domain.ReadModel.HaushaltsbuchEinzahlung;
using Haushaltsbuch.WebApi.Models.Dto.Commands;
using Haushaltsbuch.WebApi.Models.Dto;

namespace Haushaltsbuch.UI.Web.Services
{
    public interface IHaushaltsbuchService
    {
        Task<bool> ErstelleHaushaltsbuch(string name, string währung);
        Task<List<Library.Domain.ReadModel.Haushaltsbuch>> GetAllHaushaltsbuecher();
        Task<Library.Domain.ReadModel.Haushaltsbuch> GetHaushaltsbuchById(string haushaltsbuchId);
        Task<Library.Domain.ReadModel.Haushaltsbuch> GetHaushaltsbuchByName(string name);
        Task<List<HaushaltsbuchAuszahlung>> GetAuszahlungen(string haushaltsbuchId);
        Task<List<HaushaltsbuchEinzahlung>> GetEinzahlungen(string haushaltsbuchId);
        Task<List<Währung>> GetWährungen();
        Task<bool> Einzahlen(string haushaltsbuchId, InHaushaltsbuchEinzahlenCommand command);
        Task<bool> Auszahlen(string haushaltsbuchId, AusHaltshaltsbuchAuszahlenCommand command);
        Task<double> Kassenbestand(string haushaltsbuchId);
        Task<bool> HaushaltsbuchUmbenennen(string haushaltsbuchId, HaushaltsbuchUmbenennenCommand command);
    }
}