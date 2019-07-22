using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haushaltsbuch.Library.Domain;
using Haushaltsbuch.Library.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Haushaltsbuch.WebApi.Controllers
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