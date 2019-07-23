using System.Collections.Generic;
using System.Threading.Tasks;
using Haushaltsbuch.Library.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Haushaltsbuch.WebApi.Haushaltsbuch.Controllers
{
    [Route(template: "api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private IEventStore EventStore { get; }

        public EventsController(
            IEventStore eventStore)
        {
            EventStore = eventStore;
        }

        [HttpGet]
        public Task<List<string>> Get() => EventStore.GetEventsRawData();
    }
}