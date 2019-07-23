using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haushaltsbuch.Domain.Haushaltsbuch;
using Haushaltsbuch.Domain.Haushaltsbuch.Services;
using Haushaltsbuch.WebApi.Haushaltsbuch.Models.Dto.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Haushaltsbuch.WebApi.Haushaltsbuch.Controllers
{
    [Route(template: "api/[controller]")]
    [ApiController]
    public class HaushaltsbuchController : ControllerBase
    {
        private IHaushaltsbuchReader HaushaltsbuchReader { get; }
        private IHaushaltsbuchWriter HaushaltsbuchWriter { get; }

        public HaushaltsbuchController(
            IHaushaltsbuchReader haushaltsbuchReader,
            IHaushaltsbuchWriter haushaltsbuchWriter)
        {
            HaushaltsbuchReader = haushaltsbuchReader;
            HaushaltsbuchWriter = haushaltsbuchWriter;
        }

        [HttpGet]
        public async Task<ActionResult<List<Domain.Haushaltsbuch.ReadModel.Haushaltsbuch>>> Get()
        {
            return (await HaushaltsbuchReader.GetAllAsync()).ToList();
        }

        [HttpGet(template: "{haushaltsbuchId}")]
        [Route(template: "HaushaltsbuchById")]
        public async Task<ActionResult<Domain.Haushaltsbuch.ReadModel.Haushaltsbuch>> Get(string haushaltsbuchId) =>
            await HaushaltsbuchReader.GetByIdAsync(id: haushaltsbuchId);

        [HttpGet(template: "search")]
        [Route(template: "HaushaltsbuchByName")]
        public async Task<ActionResult<Domain.Haushaltsbuch.ReadModel.Haushaltsbuch>> GetByName([FromQuery] string name) =>
            await HaushaltsbuchReader.GetByName(name: name);

        [HttpPost]
        public async Task Post([FromBody] ErstelleHaushaltsbuchCommand command)
        {
            Währung währung = await HaushaltsbuchReader.GetWährungByName(name: command.Währung);

            await HaushaltsbuchWriter.ErstellenAsync(name: command.Name, währung: währung);
        }

        [HttpPut(template: "{haushaltsbuchId}/name")]
        public async Task PutName(string haushaltsbuchId, [FromBody] HaushaltsbuchUmbenennenCommand command)
        {
            await HaushaltsbuchWriter.Umbenennen(haushaltsbuchId: haushaltsbuchId, neuerName: command.NeuerName);
        }
    }
}
