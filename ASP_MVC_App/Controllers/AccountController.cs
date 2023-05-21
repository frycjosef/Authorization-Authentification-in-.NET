using System.Web;
using ASP_MVC_App.Keycloak;
using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;


namespace ASP_MVC_App.Controllers;

[Route("account")]
public class AccountController : BaseController
{ 

    public AccountController(KeycloakService keycloakService, IConfiguration configuration) : base(configuration, keycloakService)
    {
    }
    
    [Route("access-denied")]
    public IActionResult AccessDenied()
    {
        return View();
    }

    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        KeycloakService.ClearCache();

        return new SignOutResult(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties
        {
            RedirectUri = "https://localhost:7161"
        });
    }
}