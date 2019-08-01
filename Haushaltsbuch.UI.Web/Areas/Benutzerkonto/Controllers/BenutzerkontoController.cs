using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Haushaltsbuch.UI.Web.Areas.Benutzerkonto.Models;
using Haushaltsbuch.UI.Web.Services.Benutzerkonto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Haushaltsbuch.UI.Web.Areas.Benutzerkonto.Controllers
{
    [Area(areaName: "Benutzerkonto")]
    [Authorize]
    public class BenutzerkontoController : Controller
    {
        private SignInManager<WebApi.Benutzerkonto.Models.Benutzerkonto> SignInManager { get; }
        private UserManager<WebApi.Benutzerkonto.Models.Benutzerkonto> UserManager { get; }
        private IBenutzerkontoService BenutzerkontoService { get; }

        public BenutzerkontoController(
            SignInManager<WebApi.Benutzerkonto.Models.Benutzerkonto> signInManager,
            UserManager<WebApi.Benutzerkonto.Models.Benutzerkonto> userManager,
            IBenutzerkontoService benutzerkontoService)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            BenutzerkontoService = benutzerkontoService;
        }

        [AllowAnonymous]
        public IActionResult Index(IEnumerable<IdentityError> errors = null)
        {
            BenutzerkontoIndexViewModel viewModel = new BenutzerkontoIndexViewModel
            {
                Errors = errors
            };
            return View(viewName: "Index", model: viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Anmelden(BenutzerkontoAnmeldenModel benutzerkontoAnmeldenModel)
        {
            SignInResult result = await SignInManager.PasswordSignInAsync(
                userName: benutzerkontoAnmeldenModel.EMailAnmeldenummer,
                password: benutzerkontoAnmeldenModel.Passwort, 
                isPersistent: false, 
                lockoutOnFailure: true);

            if (result.Succeeded)
            {
                return Redirect(url: "/");
            }

            List<IdentityError> errors = new List<IdentityError>()
            {
                new IdentityError {Code = "COULD_NOT_LOGON", Description = "Anmeldung war nicht erfolgreich"}
            };

            return Index(errors: errors);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Registrierung(BenutzerkontoRegistrierenModel model)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            string anmeldenummer = await BenutzerkontoService.GenerateAnmeldenummer(cancellationToken: cancellationTokenSource.Token);

            WebApi.Benutzerkonto.Models.Benutzerkonto benutzerkonto = new WebApi.Benutzerkonto.Models.Benutzerkonto()
            {
                Email = model.EMail,
                UserName = anmeldenummer
            };

            IdentityResult identity = await UserManager.CreateAsync(user: benutzerkonto,
                password: model.Passwort);
            if (identity.Succeeded)
            {
                SignInResult result = await SignInManager.PasswordSignInAsync(
                    userName: model.EMail,
                    password: model.Passwort,
                    isPersistent: false,
                    lockoutOnFailure: true);
                return result.Succeeded
                    ? RedirectToAction(actionName: "Index", controllerName: "Home")
                    : RedirectToAction(actionName: "Index");
            }

            return RedirectToAction(actionName: "Index", routeValues: new
            {
                ErrorMessages = identity.Errors
            });
        }

        public async Task<IActionResult> Abmelden()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction(actionName: "Index");
        }
    }
}