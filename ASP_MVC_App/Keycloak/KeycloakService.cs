using System.Net;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;

namespace ASP_MVC_App.Keycloak;

public class KeycloakService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public KeycloakService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public void Logout()
    {
        var client = new HttpClient();

        client.BaseAddress = new Uri("http://localhost:8080/realms/BP/protocol/openid-connect/logout");
        
        var user = _httpContextAccessor.HttpContext.GetTokenAsync("access_token").Result;
        
        var var1 = _configuration["Keycloak:client_id"];
        var var2 = _httpContextAccessor.HttpContext.GetTokenAsync("refresh_token").Result;

        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("client_id", _configuration["Keycloak:client_id"]),
            // new KeyValuePair<string, string>("refresh_token", _httpContextAccessor.HttpContext.GetTokenAsync("refresh_token").Result),
            // new KeyValuePair<string, string>("post_logout_redirect_uri", "https://localhost:7161")
        });

        var responseTask = client.PostAsync("", content );
        responseTask.Wait();
        
        var httpResponse = responseTask.Result;
    }

    public bool Authorize(string permissionName)
    {
        var client = new HttpClient();

        var user = _httpContextAccessor.HttpContext.GetTokenAsync("access_token").Result;
        
        client.BaseAddress = new Uri(_configuration["Keycloak:token-introspect-endpoint"]);
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("token", _httpContextAccessor.HttpContext.GetTokenAsync("access_token").Result),
            new KeyValuePair<string, string>("client_id", _configuration["Keycloak:client_id"]),
            new KeyValuePair<string, string>("client_secret", _configuration["Keycloak:client_secret"])
        });
        
        var responseTask = client.PostAsync("", content);
        responseTask.Wait();

        var httpResponse = responseTask.Result;
        
        if(httpResponse.StatusCode == HttpStatusCode.OK)
        {
            var result = httpResponse.Content.ReadAsStringAsync().Result;

            Token token = new Token(result);
        
            return token.ClientPermissions.Contains(permissionName) || token.RealmPermissions.Contains(permissionName);
        }

        return false;
    }
}