using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Memory;

namespace ASP_MVC_App.Keycloak;

public class KeycloakService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMemoryCache _cache;
    private static string _permissionsCacheKey = "Permissions";
    
    public KeycloakService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _cache = cache;
    }
    
    public void ClearCache()
    {
        _cache.Remove($"{_permissionsCacheKey}");
    }

    public bool Authorize(string permissionName)
    {
        //Cache permissions
        if (!_cache.TryGetValue($"{_permissionsCacheKey}",
                out Token? token) || token == null || (!token.ClientPermissions.Any() && !token.RealmPermissions.Any()))
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

                token = new Token(result);
                
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));
                
                _cache.Set($"{_permissionsCacheKey}", token, cacheEntryOptions);
            }
            else
            {
                token = null;
            }
        }

        if (token is null) 
            return false;
        
        // Check for exact permission
        if(token.ClientPermissions.Contains(permissionName) ||  token.RealmPermissions.Contains(permissionName))
        {
            return true;
        }
        
        // Check for wildcard permission
        if (permissionName.EndsWith(".*"))
        {
            string basePermission = permissionName.Substring(0, permissionName.Length - 2);
            if (token.ClientPermissions.Contains(basePermission) || token.RealmPermissions.Contains(basePermission))
            {
                return true;
            }

            // Check for specific permissions with the same base permission
            string specificPermissionPrefix = basePermission + ".";
            foreach (string clientPermission in token.ClientPermissions)
            {
                if (clientPermission.StartsWith(specificPermissionPrefix))
                {
                    return true;
                }
            }

            foreach (string realmPermission in token.RealmPermissions)
            {
                if (realmPermission.StartsWith(specificPermissionPrefix))
                {
                    return true;
                }
            }
        }

        // Check for all permission levels
        while (permissionName.Contains("."))
        {
            int lastDotIndex = permissionName.LastIndexOf('.');
            permissionName = permissionName.Substring(0, lastDotIndex);

            if (token.ClientPermissions.Contains(permissionName) || token.RealmPermissions.Contains(permissionName))
            {
                return true;
            }
        }

        return false;
    }
}