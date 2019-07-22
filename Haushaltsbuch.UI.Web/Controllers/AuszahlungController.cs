using System.Threading.Tasks;
using Haushaltsbuch.UI.Web.Models;
using Haushaltsbuch.UI.Web.Services;
using Haushaltsbuch.WebApi.Models.Dto.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Haushaltsbuch.UI.Web.Controllers
{
    public class AuszahlungController : Controller
    {
        private IHaushaltsbuchService HaushaltsbuchService { get; }

        public AuszahlungController
            (IHaushaltsbuchService haushaltsbuchService)
        {
            HaushaltsbuchService = haushaltsbuchService;
        }

        public async Task<IActionResult> Add(string haushaltsbuchId)
        {
            Library.Domain.ReadModel.Haushaltsbuch haushaltsbuch = await HaushaltsbuchService.GetHaushaltsbuchById(haushaltsbuchId: haushaltsbuchId);
            AuszahlungAddViewModel viewModel = new AuszahlungAddViewModel()
            {
                HaushaltsbuchId = haushaltsbuchId,
                WährungSymbol = haushaltsbuch.WährungSymbol,
                WährungName = haushaltsbuch.WährungName,
                HaushaltsbuchName = haushaltsbuch.Name
            };
            return View(model: viewModel);
        }

        public async Task<IActionResult> Save(AuszahlungAddViewModel model)
        {
            await HaushaltsbuchService.Auszahlen(
                haushaltsbuchId: model.HaushaltsbuchId,
                command: new AusHaltshaltsbuchAuszahlenCommand(
                    betrag: model.Betrag,
                    auszahlungsdatum: model.Auszahlungsdatum,
                    kategorie: model.Kategorie,
                    memotext: model.Memotext));

            return RedirectToAction(actionName: "Detail", controllerName: "Haushaltsbuch", routeValues: new
            {
                haushaltsbuchId = model.HaushaltsbuchId
            });
        }
    }
}