using System.Threading.Tasks;
using Haushaltsbuch.Domain.Benutzerkonto;
using Haushaltsbuch.Domain.Benutzerkonto.Services;
using Haushaltsbuch.WebApi.Benutzerkonto.Models;
using Microsoft.AspNetCore.Mvc;

namespace Haushaltsbuch.WebApi.Benutzerkonto.Controllers
{
    [Route(template: "api/[controller]")]
    [ApiController]
    public class BenutzerkontoController : ControllerBase
    {
        private IBenutzerkontoReader BenutzerkontoReader { get; }
        private IBenutzerkontoWriter BenutzerkontoWriter { get; }

        public BenutzerkontoController(
            IBenutzerkontoReader benutzerkontoReader,
            IBenutzerkontoWriter benutzerkontoWriter)
        {
            BenutzerkontoReader = benutzerkontoReader;
            BenutzerkontoWriter = benutzerkontoWriter;
        }

        [HttpPost]
        public async Task<ActionResult> Registrieren([FromBody] RegistriereBenutzerModel model)
        {
            await BenutzerkontoWriter.Registrieren(
                anmeldenummer: model.Anmeldenummer,
                email: model.EMail,
                passwortHash: model.PasswortHash,
                securityStamp: model.SecurityStamp);

            return Ok();
        }

        [HttpGet(template: "findbyanmeldenummer/{anmeldenummer}")]
        public async Task<ActionResult<Models.Benutzerkonto>> FindByAnmeldenummer(string anmeldenummer)
        {
            Domain.Benutzerkonto.ReadModel.Benutzerkonto benutzerkonto = await BenutzerkontoReader.GetByAnmeldenummer(anmeldenummer: anmeldenummer);
            if (benutzerkonto == null)
            {
                return null;
            }
            
            return new Models.Benutzerkonto
            {
                Id = new BenutzerkontoId(id: benutzerkonto.Id).Id,
                Email = benutzerkonto.EMail,
                UserName = benutzerkonto.Anmeldenummer
            };
        }

        [HttpPut(template: "{anmeldenummer}/normalisierteemail")]
        public async Task<ActionResult> SetzeNormalisierteEMailAdresse(string anmeldenummer, [FromBody] SetzeNormalisierteEMailAdresseModel model)
        {
            Domain.Benutzerkonto.ReadModel.Benutzerkonto benutzerkonto = await BenutzerkontoReader.GetByAnmeldenummer(anmeldenummer: anmeldenummer);
            if (benutzerkonto == null)
            {
                return null;
            }

            await BenutzerkontoWriter.SetzeNormalisierteEMailAdresse(
                benutzerkontoId: benutzerkonto.Id,
                anmeldenummer: anmeldenummer,
                normalisierteEMailAdresse: model.NormalisierteEMailAdresse);

            return Ok();
        }

        [HttpGet(template: "{anmeldenummer}/securitystamp")]
        public async Task<ActionResult<string>> LiefereSecurityStamp(string anmeldenummer)
        {
            Domain.Benutzerkonto.ReadModel.Benutzerkonto benutzerkonto = await BenutzerkontoReader.GetByAnmeldenummer(anmeldenummer: anmeldenummer);
            return benutzerkonto?.SecurityStamp;
        }

        [HttpGet(template: "{anmeldenummer}/email")]
        public async Task<ActionResult<string>> LiefereEMail(string anmeldenummer)
        {
            Domain.Benutzerkonto.ReadModel.Benutzerkonto benutzerkonto = await BenutzerkontoReader.GetByAnmeldenummer(anmeldenummer: anmeldenummer);
            return benutzerkonto?.EMail;
        }

        [HttpGet(template: "{anmeldenummer}/username")]
        public async Task<ActionResult<string>> LiefereUserName(string anmeldenummer)
        {
            Domain.Benutzerkonto.ReadModel.Benutzerkonto benutzerkonto = await BenutzerkontoReader.GetByAnmeldenummer(anmeldenummer: anmeldenummer);
            return benutzerkonto?.Anmeldenummer;
        }

        [HttpGet(template: "{anmeldenummer}/userid")]
        public async Task<ActionResult<string>> LiefereUserId(string anmeldenummer)
        {
            Domain.Benutzerkonto.ReadModel.Benutzerkonto benutzerkonto = await BenutzerkontoReader.GetByAnmeldenummer(anmeldenummer: anmeldenummer);
            return benutzerkonto?.Id;
        }

        [HttpGet(template: "{anmeldenummer}/passwordhash")]
        public async Task<ActionResult<string>> LieferePasswordHash(string anmeldenummer)
        {
            Domain.Benutzerkonto.ReadModel.Benutzerkonto benutzerkonto = await BenutzerkontoReader.GetByAnmeldenummer(anmeldenummer: anmeldenummer);
            return benutzerkonto?.PasswortHash;
        }

        [HttpGet(template: "suche/email/{email}")]
        public async Task<ActionResult<Models.Benutzerkonto>> SucheAnhandEMail(string email)
        {
            Domain.Benutzerkonto.ReadModel.Benutzerkonto benutzerkonto = await BenutzerkontoReader.GetByEMail(email: email);
            
            if (benutzerkonto == null)
            {
                return null;
            }
            
            return new Models.Benutzerkonto
            {
                Id = new BenutzerkontoId(id: benutzerkonto.Id).Id,
                Email = benutzerkonto.EMail,
                UserName = benutzerkonto.Anmeldenummer
            };
        }
    }
}