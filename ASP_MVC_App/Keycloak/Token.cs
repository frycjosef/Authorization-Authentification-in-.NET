using Newtonsoft.Json.Linq;

namespace ASP_MVC_App.Keycloak;

public class Token
{
    public Token(string json)
    {
        JObject jObject = JObject.Parse(json);
        JToken jRealmPermissions =jObject["realm_access"]?["roles"];
        RealmPermissions = jRealmPermissions == null ? jRealmPermissions.Select(p => p.ToString()).ToArray() : new string[]{};
        JToken jClientPermissions = jObject["resource_access"]?["account"]?["roles"];
        ClientPermissions = jRealmPermissions == null ? jClientPermissions.Select(p => p.ToString()).ToArray() : new string[]{};
    }

    public string[] RealmPermissions { get; set; }
    public string[] ClientPermissions { get; set; }
}