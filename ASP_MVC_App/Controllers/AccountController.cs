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
    
    public IActionResult AccessDenied()
    {
        return View();
    }

    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        // KeycloakService.Logout();
        // await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        // await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        // Request.HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

        // return Redirect("http://localhost:8080/realms/BP/protocol/openid-connect/logout?client_id=" +
        //                 Configuration.GetSection("KeyCloak:client_id").Value);

        return new SignOutResult(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties
        {
            RedirectUri = "https://localhost:7161"
        });
        // return new SignOutResult(OpenIdConnectDefaults.AuthenticationScheme, new OpenIdConnectChallengeProperties(new Dictionary<string, string?>()
        // {
        //     {"post_logout_redirect_uri", "https://localhost:7161"},
        //     {"id_token_hint", HttpContext.GetTokenAsync("id_token").Result}
        // }));
    }
}