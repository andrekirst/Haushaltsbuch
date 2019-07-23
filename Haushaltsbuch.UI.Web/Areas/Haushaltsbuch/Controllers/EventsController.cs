using System.Threading.Tasks;
using Haushaltsbuch.UI.Web.Areas.Haushaltsbuch.Models;
using Haushaltsbuch.UI.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Haushaltsbuch.UI.Web.Areas.Haushaltsbuch.Controllers
{
    [Area(areaName: "Haushaltsbuch")]
    public class EventsController : Controller
    {
        private IEventsService EventsService { get; }

        public EventsController(
            IEventsService eventsService)
        {
            EventsService = eventsService;
        }

        public async Task<IActionResult> Index()
        {
            EventsIndexViewModel viewModel = new EventsIndexViewModel
            {
                Events = await EventsService.GetEventsRawData()
            };

            return View(model: viewModel);
        }
    }
}