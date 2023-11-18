using BP.WebApp.Keycloak;

namespace BP.WebApp.Models;

public static class PermissionHelper
{
    public static bool HasPermission(string permission, HttpContext context)
    {
        var keyCloakService = context.RequestServices.GetService(typeof(KeycloakService)) as KeycloakService;
        if(keyCloakService == null)
            return false;
        
        return keyCloakService.Authorize(permission);
    }
}