using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Haushaltsbuch.UI.Web.Areas.Benutzerkonto.Controllers
{
    [Area(areaName: "Benutzerkonto")]
    public class BenutzerkontoController : Controller
    {
        public BenutzerkontoController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginGoogle(string redirectUri)
        {
            return Challenge(properties: new AuthenticationProperties
            {
                RedirectUri = redirectUri
            },
            GoogleDefaults.AuthenticationScheme);
        }

        public IActionResult LoginMicrosoft(string redirectUri = null)
        {
            return Challenge(properties: new AuthenticationProperties
            {
                RedirectUri = redirectUri
            },
            "Azure AD / Microsoft");
        }

        public IActionResult LogoutMicrosoft(string callbackUrl)
        {
            return SignOut(
                properties: new AuthenticationProperties { RedirectUri = callbackUrl },
                CookieAuthenticationDefaults.AuthenticationScheme,
                MicrosoftAccountDefaults.AuthenticationScheme);
        }
    }
}