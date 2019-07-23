using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haushaltsbuch.Domain.Haushaltsbuch;
using Haushaltsbuch.Domain.Haushaltsbuch.Services;
using Microsoft.AspNetCore.Mvc;

namespace Haushaltsbuch.WebApi.Haushaltsbuch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaehrungenController : ControllerBase
    {
        private IHaushaltsbuchReader HaushaltsbuchReader { get; }

        public WaehrungenController(
            IHaushaltsbuchReader haushaltsbuchReader)
        {
            HaushaltsbuchReader = haushaltsbuchReader;
        }

        [HttpGet]
        public async Task<List<Währung>> Get()
        {
            IEnumerable<Währung> währungen = await HaushaltsbuchReader.GetAllWährungen();

            return währungen.ToList();
        }
    }
}