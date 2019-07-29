using System.Threading.Tasks;
using Haushaltsbuch.UI.Web.Areas.Benutzerkonto.Models;
using Microsoft.AspNetCore.Authentication;
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

        public BenutzerkontoController(
            SignInManager<WebApi.Benutzerkonto.Models.Benutzerkonto> signInManager,
            UserManager<WebApi.Benutzerkonto.Models.Benutzerkonto> userManager)
        {
            SignInManager = signInManager;
            UserManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Anmelden(BenutzerkontoAnmeldenModel benutzerkontoAnmeldenModel)
        {
            SignInResult result = await SignInManager.PasswordSignInAsync(
                userName: benutzerkontoAnmeldenModel.EMailAnmeldenummer,
                password: benutzerkontoAnmeldenModel.Passwort, 
                isPersistent: true, 
                lockoutOnFailure: true);

            return result.Succeeded
                ? RedirectToAction(actionName: "Index", controllerName: "Home")
                : RedirectToAction(actionName: "Index");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Registrierung(BenutzerkontoRegistrierenModel benutzerkontoRegistrierenModel)
        {
            IdentityResult identity = await UserManager.CreateAsync(user: new WebApi.Benutzerkonto.Models.Benutzerkonto()
                {
                },
                password: benutzerkontoRegistrierenModel.Passwort);
            if (identity.Succeeded)
            {
                var result = await SignInManager.PasswordSignInAsync(
                    userName: benutzerkontoRegistrierenModel.EMail,
                    password: benutzerkontoRegistrierenModel.Passwort,
                    isPersistent: true,
                    lockoutOnFailure: true);
                return result.Succeeded
                    ? RedirectToAction(actionName: "Index", controllerName: "Home")
                    : RedirectToAction(actionName: "Index");
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Abmelden()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction(actionName: "Index");
        }
    }
}