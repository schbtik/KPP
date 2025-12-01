using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ProcureRiskAnalyzer.Web.Controllers
{
    public class AuthController : Controller
    {
        [Authorize]
        public IActionResult Profile() => View();

        public IActionResult Login()
        {
            return Challenge(
                new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                {
                    RedirectUri = "/Auth/Profile"
                },
                Okta.AspNetCore.OktaDefaults.MvcAuthenticationScheme);
        }

        public IActionResult Logout()
        {
            return SignOut(
                new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                {
                    RedirectUri = "/"
                },
                Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme,
                Okta.AspNetCore.OktaDefaults.MvcAuthenticationScheme);
        }
    }
}
