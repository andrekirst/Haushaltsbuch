using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Haushaltsbuch.Library.Domain;
using Microsoft.AspNetCore.Mvc;
using Haushaltsbuch.UI.Web.Models;
using Haushaltsbuch.UI.Web.Services;
using Microsoft.AspNetCore.Http;

namespace Haushaltsbuch.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private IHaushaltsbuchService HaushaltsbuchService { get; }

        public HomeController(
            IHaushaltsbuchService haushaltsbuchService)
        {
            HaushaltsbuchService = haushaltsbuchService;
        }

        public async Task<IActionResult> Index()
        {
            List<Library.Domain.ReadModel.Haushaltsbuch> haushaltsbücher = await HaushaltsbuchService.GetAllHaushaltsbuecher();
            HomeIndexViewModel viewModel = new HomeIndexViewModel
            {
                Haushaltsbücher = haushaltsbücher
            };
            return View(model: viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult System()
        {
            Dictionary<string, string> environmentVariables = new Dictionary<string, string>();
            foreach (DictionaryEntry environmentVariable in Environment.GetEnvironmentVariables())
            {
                environmentVariables.Add(key: environmentVariable.Key.ToString(), value: environmentVariable.Value.ToString());
            }

            HostString hostString = HttpContext.Request.Host;

            HomeSystemViewModel viewModel = new HomeSystemViewModel
            {
                EnvironmentVariables = environmentVariables,
                Hostname = $"{hostString.Host}:{hostString.Port}"
            };
            return View(model: viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(model: new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
