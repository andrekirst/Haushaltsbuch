using System.Collections.Generic;
using System.Threading.Tasks;
using Haushaltsbuch.Library.Domain;
using Haushaltsbuch.Library.Domain.Services;
using Haushaltsbuch.WebApi.Models.Dto.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Haushaltsbuch.WebApi.Controllers
{
    [Route(template: "api/[controller]")]
    [ApiController]
    public class AuszahlungenController : ControllerBase
    {
        private IHaushaltsbuchWriter HaushaltsbuchWriter { get; }
        private IHaushaltsbuchReader HaushaltsbuchReader { get; }

        public AuszahlungenController(
            IHaushaltsbuchWriter haushaltsbuchWriter,
            IHaushaltsbuchReader haushaltsbuchReader)
        {
            HaushaltsbuchWriter = haushaltsbuchWriter;
            HaushaltsbuchReader = haushaltsbuchReader;
        }

        [HttpPost(template: "{haushaltsbuchId}")]
        public Task Post(string haushaltsbuchId, [FromBody] AusHaltshaltsbuchAuszahlenCommand command) =>
            HaushaltsbuchWriter.AuszahlenAsync(
                haushaltsbuchId: haushaltsbuchId,
                betrag: command.Betrag,
                auszahlungsdatum: command.Auszahlungsdatum,
                kategorie: new Kategorie(name: command.Kategorie),
                memotext: command.Memotext);

        [HttpGet(template: "{haushaltsbuchId}")]
        public async Task<ActionResult<List<HaushaltsbuchAuszahlung>>> GetAuszahlungen(string haushaltsbuchId)
        {
            IEnumerable<Library.Domain.ReadModel.HaushaltsbuchAuszahlung> auszahlungen = await HaushaltsbuchReader.GetAuszahlungenOfAsync(haushaltsbuchId: haushaltsbuchId);
            return Ok(value: auszahlungen);
        }
    }
}