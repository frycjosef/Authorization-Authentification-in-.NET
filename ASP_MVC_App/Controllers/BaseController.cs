using ASP_MVC_App.Keycloak;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ASP_MVC_App.Controllers;

public class BaseController : Controller
{
    protected readonly IConfiguration Configuration;
    protected KeycloakService KeycloakService;

    public BaseController(IConfiguration configuration, KeycloakService service)
    {
        Configuration = configuration;
        KeycloakService = service;
    }

    public bool Authorize(string permission)
    {
        return KeycloakService.Authorize(permission);
    }
}