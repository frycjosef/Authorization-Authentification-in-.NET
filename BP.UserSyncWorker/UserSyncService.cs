using System.Net;
using System.Net.Http.Headers;
using BP.Common.Data;
using BP.Common.Data.Entities;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KeycloakUserSyncWorker;

public class UserSyncService
{
    private readonly BPContext _dbContext;
    private readonly string _tokenKey = "token";
    private readonly IMemoryCache _cache;


    public UserSyncService(BPContext dbContext)
    {
        _dbContext = dbContext;
        _cache = new MemoryCache(new MemoryCacheOptions());
    }

    public void SyncUsers()
    {
        try
        {
            var usersFromKeycloak = FetchUsersFromKeycloak();
            UpdateDatabaseWithUsers(usersFromKeycloak);
            
            Console.WriteLine("User synchronization completed at: " + DateTime.Now);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error during synchronization: " + ex.Message);
        }
    }

    private List<User> FetchUsersFromKeycloak()
    {
        if (!_cache.TryGetValue($"{_tokenKey}",
                out string token) || token == null || !String.IsNullOrWhiteSpace(token))
        {
            HttpClient authClient = new HttpClient();
            authClient.BaseAddress = new Uri("http://localhost:8080/realms/master/protocol/openid-connect/token");
            
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", "admin"),
                new KeyValuePair<string, string>("password", "admin"),
                new KeyValuePair<string, string>("client_id", "admin-cli"),
                new KeyValuePair<string, string>("grant_type", "password")
            });
            
            var authResponseTask = authClient.PostAsync("", content);
            authResponseTask.Wait();

            var authHttpResponse = authResponseTask.Result;
            
            if(authHttpResponse.StatusCode == HttpStatusCode.OK)
            {
                JObject jObject = JObject.Parse(authHttpResponse.Content.ReadAsStringAsync().Result);
                
                token = jObject["access_token"].ToString();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));
                
                _cache.Set($"{_tokenKey}", token, cacheEntryOptions);
            } 
            else
            {
                token = null;
            }
        }
        
        
        
        var client = new HttpClient();
        
        client.BaseAddress = new Uri("http://localhost:8080/admin/realms/BP/users");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var responseTask = client.GetAsync("").Result;

        var httpResponse = responseTask;

        var users = new List<User>();
        
        if (httpResponse.StatusCode == HttpStatusCode.OK)
        {
            var result = httpResponse.Content.ReadAsStringAsync().Result;
            
            users = JsonConvert.DeserializeObject<List<User>>(result);
        }
        else
        {
            throw new Exception("Error fetching users from Keycloak");
        }
        
        return users;
    }

    private void UpdateDatabaseWithUsers(List<User> usersFromKeycloak)
    {
        var dbUsers = _dbContext.Users.ToList();

        // Find users to delete
        var usersToDelete = dbUsers
            .Where(dbUser => !usersFromKeycloak.Any(keycloakUser => keycloakUser.Id == dbUser.Id))
            .ToList();

        // Delete users
        _dbContext.Users.RemoveRange(usersToDelete);

        // Find users to add
        var usersToAdd = usersFromKeycloak
            .Where(keycloakUser => !dbUsers.Any(dbUser => dbUser.Id == keycloakUser.Id))
            .ToList();

        // Add users
        _dbContext.Users.AddRange(usersToAdd);
        
        var usersToUpdate = usersFromKeycloak
            .Where(keycloakUser => dbUsers.Any(dbUser => dbUser.Id == keycloakUser.Id))
            .ToList();

        foreach (var user in usersToUpdate)
        {
            var dbUser = dbUsers.First(dbUser => dbUser.Id == user.Id);
            
            dbUser.Username = user.Username;
            dbUser.Email = user.Email;
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
        }

        // Save changes to the database
        _dbContext.SaveChanges();
    }
}