using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haushaltsbuch.Library.Domain.Commands;
using Haushaltsbuch.Library.Domain.ReadModel;
using Haushaltsbuch.Library.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Haushaltsbuch.WebApi.Controllers
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