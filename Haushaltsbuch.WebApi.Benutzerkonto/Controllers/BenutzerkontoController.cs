using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Haushaltsbuch.WebApi.Benutzerkonto.Controllers
{
    [Route(template: "api/[controller]")]
    [ApiController]
    public class BenutzerkontoController : ControllerBase
    {
        public BenutzerkontoController()
        {
        }

        [HttpGet(template: "findbyname/{anmeldenummer}")]
        public async Task<ActionResult<Models.Benutzerkonto>> FindByName(string anmeldenummer)
        {
            return new ActionResult<Models.Benutzerkonto>(new Models.Benutzerkonto()
            {
                Email = "test@test.de"
            });
        }
    }
}