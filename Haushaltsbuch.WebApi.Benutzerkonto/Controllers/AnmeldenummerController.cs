using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haushaltsbuch.Domain.Benutzerkonto.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Haushaltsbuch.WebApi.Benutzerkonto.Controllers
{
    [Route(template: "api/[controller]")]
    [ApiController]
    public class AnmeldenummerController : ControllerBase
    {
        private IAnmeldenummerGenerator AnmeldenummerGenerator { get; }

        public AnmeldenummerController(
            IAnmeldenummerGenerator anmeldenummerGenerator)
        {
            AnmeldenummerGenerator = anmeldenummerGenerator;
        }

        [HttpGet(template: "generate")]
        public async Task<ActionResult<string>> Generate() => AnmeldenummerGenerator.Generate();
    }
}