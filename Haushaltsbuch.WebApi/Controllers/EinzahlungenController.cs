using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haushaltsbuch.Domain.Haushaltsbuch.Commands;
using Haushaltsbuch.Domain.Haushaltsbuch.ReadModel;
using Haushaltsbuch.Domain.Haushaltsbuch.Services;
using Microsoft.AspNetCore.Mvc;

namespace Haushaltsbuch.WebApi.Haushaltsbuch.Controllers
{
    [Route(template: "api/[controller]")]
    [ApiController]
    public class EinzahlungenController : ControllerBase
    {
        private IHaushaltsbuchWriter HaushaltsbuchWriter { get; }
        private IHaushaltsbuchReader HaushaltsbuchReader { get; }

        public EinzahlungenController(
            IHaushaltsbuchWriter haushaltsbuchWriter,
            IHaushaltsbuchReader haushaltsbuchReader)
        {
            HaushaltsbuchWriter = haushaltsbuchWriter;
            HaushaltsbuchReader = haushaltsbuchReader;
        }

        [HttpPost(template: "{haushaltsbuchId}")]
        public Task Post(string haushaltsbuchId, [FromBody] InHaushaltsbuchEinzahlenCommand command) =>
            HaushaltsbuchWriter.EinzahlenAsync(
                haushaltsbuchId: haushaltsbuchId,
                betrag: command.Betrag,
                einzahlungsdatum: command.EinzahlungsDatum);

        [HttpGet(template: "{haushaltsbuchId}")]
        public async Task<ActionResult<List<HaushaltsbuchEinzahlung>>> GetEinzahlungen(string haushaltsbuchId)
        {
            IEnumerable<HaushaltsbuchEinzahlung> einzahlungen = await HaushaltsbuchReader.GetEinzahlungenOfAsync(haushaltsbuchId: haushaltsbuchId);
            return einzahlungen.ToList();
        }
    }
}