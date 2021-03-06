﻿using System.Threading.Tasks;
using Haushaltsbuch.UI.Web.Areas.Haushaltsbuch.Models;
using Haushaltsbuch.UI.Web.Services;
using Haushaltsbuch.WebApi.Haushaltsbuch.Models.Dto.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Haushaltsbuch.UI.Web.Areas.Haushaltsbuch.Controllers
{
    [Area(areaName: "Haushaltsbuch")]
    public class EinzahlungController : Controller
    {
        private IHaushaltsbuchService HaushaltsbuchService { get; }

        public EinzahlungController(
            IHaushaltsbuchService haushaltsbuchService)
        {
            HaushaltsbuchService = haushaltsbuchService;
        }

        public async Task<IActionResult> Add(string haushaltsbuchId)
        {
            Domain.Haushaltsbuch.ReadModel.Haushaltsbuch haushaltsbuch = await HaushaltsbuchService.GetHaushaltsbuchById(haushaltsbuchId: haushaltsbuchId);
            EinzahlungAddViewModel viewModel = new EinzahlungAddViewModel
            {
                HaushaltsbuchId = haushaltsbuchId,
                WährungSymbol = haushaltsbuch.WährungSymbol,
                WährungName = haushaltsbuch.WährungName,
                HaushaltsbuchName = haushaltsbuch.Name
            };
            return View(model: viewModel);
        }

        public async Task<IActionResult> Save(EinzahlungAddViewModel model)
        {
            await HaushaltsbuchService.Einzahlen(
                haushaltsbuchId: model.HaushaltsbuchId,
                command: new InHaushaltsbuchEinzahlenCommand(betrag: model.Betrag, einzahlungsdatum: model.Einzahlungdatum));

            return RedirectToAction(actionName: "Detail", controllerName: "Haushaltsbuch", routeValues: new
            {
                haushaltsbuchId = model.HaushaltsbuchId
            });
        }
    }
}