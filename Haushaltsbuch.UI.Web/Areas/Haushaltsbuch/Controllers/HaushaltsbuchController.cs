using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haushaltsbuch.UI.Web.Areas.Haushaltsbuch.Models;
using Haushaltsbuch.UI.Web.Services;
using Haushaltsbuch.WebApi.Haushaltsbuch.Models.Dto;
using Haushaltsbuch.WebApi.Haushaltsbuch.Models.Dto.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HaushaltsbuchAuszahlung = Haushaltsbuch.Domain.Haushaltsbuch.ReadModel.HaushaltsbuchAuszahlung;
using HaushaltsbuchEinzahlung = Haushaltsbuch.Domain.Haushaltsbuch.ReadModel.HaushaltsbuchEinzahlung;

namespace Haushaltsbuch.UI.Web.Areas.Haushaltsbuch.Controllers
{
    [Area(areaName: "Haushaltsbuch")]
    public class HaushaltsbuchController : Controller
    {
        private IHaushaltsbuchService HaushaltsbuchService { get; }

        public HaushaltsbuchController(IHaushaltsbuchService haushaltsbuchService)
        {
            HaushaltsbuchService = haushaltsbuchService;
        }

        public async Task<IActionResult> Detail(string haushaltsbuchId)
        {
            var taskHaushaltsbuch = HaushaltsbuchService.GetHaushaltsbuchById(haushaltsbuchId: haushaltsbuchId);
            var taskEinzahlungen = HaushaltsbuchService.GetEinzahlungen(haushaltsbuchId: haushaltsbuchId);
            var taskAuszahlungen = HaushaltsbuchService.GetAuszahlungen(haushaltsbuchId: haushaltsbuchId);

            await Task.WhenAll(taskAuszahlungen, taskEinzahlungen, taskHaushaltsbuch);

            HaushaltsbuchIndexViewModel viewModel = new HaushaltsbuchIndexViewModel
            {
                Haushaltsbuch = await taskHaushaltsbuch,
                Einzahlungen = (await taskEinzahlungen) ?? new List<HaushaltsbuchEinzahlung>(),
                Auszahlungen = (await taskAuszahlungen) ?? new List<HaushaltsbuchAuszahlung>()
            };

            return View(model: viewModel);
        }

        public async Task<IActionResult> Add()
        {
            List<Währung> währungen = (await HaushaltsbuchService.GetWährungen()).ToList();

            HaushaltsbuchAddViewModel viewModel = new HaushaltsbuchAddViewModel
            {
                Währungen = währungen,
                WährungenItems = währungen.Select(selector: währung => new SelectListItem(text: währung.Symbol, value: währung.Name)).ToList()
            };
            return View(model: viewModel);
        }

        public async Task<IActionResult> Save(HaushaltsbuchAddViewModel model)
        {
            bool successfullyCreated = await HaushaltsbuchService.ErstelleHaushaltsbuch(name: model.Name, währung: model.NeueWährung);

            if (successfullyCreated)
            {
                 Domain.Haushaltsbuch.ReadModel.Haushaltsbuch haushaltsbuch = await HaushaltsbuchService.GetHaushaltsbuchByName(name: model.Name);

                return RedirectToAction(actionName: "Detail", routeValues: new
                {
                    haushaltsbuchId = haushaltsbuch.Id
                });
            }

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        public async Task<IActionResult> UpdateName(string haushaltsbuchId, string name)
        {
            bool result = await HaushaltsbuchService.HaushaltsbuchUmbenennen(
                haushaltsbuchId: haushaltsbuchId,
                command: new HaushaltsbuchUmbenennenCommand(neuerName: name));

            return result ? Ok() : StatusCode(500);
        }
    }
}