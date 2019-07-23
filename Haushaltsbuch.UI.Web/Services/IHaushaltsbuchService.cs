using System.Collections.Generic;
using System.Threading.Tasks;
using Haushaltsbuch.WebApi.Haushaltsbuch.Models.Dto;
using Haushaltsbuch.WebApi.Haushaltsbuch.Models.Dto.Commands;
using HaushaltsbuchAuszahlung = Haushaltsbuch.Domain.Haushaltsbuch.ReadModel.HaushaltsbuchAuszahlung;
using HaushaltsbuchEinzahlung = Haushaltsbuch.Domain.Haushaltsbuch.ReadModel.HaushaltsbuchEinzahlung;

namespace Haushaltsbuch.UI.Web.Services
{
    public interface IHaushaltsbuchService
    {
        Task<bool> ErstelleHaushaltsbuch(string name, string währung);
        Task<List<Domain.Haushaltsbuch.ReadModel.Haushaltsbuch>> GetAllHaushaltsbuecher();
        Task<Domain.Haushaltsbuch.ReadModel.Haushaltsbuch> GetHaushaltsbuchById(string haushaltsbuchId);
        Task<Domain.Haushaltsbuch.ReadModel.Haushaltsbuch> GetHaushaltsbuchByName(string name);
        Task<List<HaushaltsbuchAuszahlung>> GetAuszahlungen(string haushaltsbuchId);
        Task<List<HaushaltsbuchEinzahlung>> GetEinzahlungen(string haushaltsbuchId);
        Task<List<Währung>> GetWährungen();
        Task<bool> Einzahlen(string haushaltsbuchId, InHaushaltsbuchEinzahlenCommand command);
        Task<bool> Auszahlen(string haushaltsbuchId, AusHaltshaltsbuchAuszahlenCommand command);
        Task<double> Kassenbestand(string haushaltsbuchId);
        Task<bool> HaushaltsbuchUmbenennen(string haushaltsbuchId, HaushaltsbuchUmbenennenCommand command);
    }
}