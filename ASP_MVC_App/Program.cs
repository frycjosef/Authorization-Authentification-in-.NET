using System.IdentityModel.Tokens.Jwt;
using ASP_MVC_App.Data;
using ASP_MVC_App.Data.Repositories;
using ASP_MVC_App.Data.Repositories.Interfaces;
using ASP_MVC_App.Keycloak;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

IdentityModelEventSource.ShowPII = true;

builder.Services.AddControllersWithViews();

builder.Services.AddMvc();

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddDbContext<BPContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("BPDatabase"));
});

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddSingleton<IRepositoryManagerFactory, RepositoryManagerFactory>();

builder.Services.AddAuthentication(options => 
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddCookie("Cookies")
    .AddOpenIdConnect(options =>
    {
        //Keycloak URL Server
        options.Authority = builder.Configuration.GetSection("KeyCloak:auth-server-url").Value; //builder.Configuration.GetSection("KeyCloak:auth-server-url").Value;
        //Client configured in Keycloak
        options.ClientId = builder.Configuration.GetSection("KeyCloak:client_id").Value;
        //Client's Secret configured in Keycloak
        options.ClientSecret = builder.Configuration.GetSection("KeyCloak:client_secret").Value;
        
        options.Scope.Add("openid");
        options.Scope.Add("profile");

        options.AccessDeniedPath = "/Account/AccessDenied";

        options.RequireHttpsMetadata = false;//Boolean.Parse(builder.Configuration.GetSection("KeyCloak:RequireHttpsMetadata").Value);;
        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;

        //OpenID flow to use
        options.ResponseType = OpenIdConnectResponseType.Code;
        
        options.ClaimActions.MapJsonKey("role", "role", "role");
        options.TokenValidationParameters.RoleClaimType = "role";
        
        options.SignedOutRedirectUri = "https://localhost:7161";

        options.Events = new OpenIdConnectEvents()
        {
            OnRedirectToIdentityProviderForSignOut = (context) =>
            {
                var logoutUri =
                    $"{builder.Configuration.GetSection("KeyCloak:auth-server-url").Value}/protocol/openid-connect/logout";

                var postLogoutUri = context.Properties.RedirectUri;

                if (!string.IsNullOrWhiteSpace(postLogoutUri))
                {
                    if (postLogoutUri.StartsWith("/"))
                    {
                        var request = context.Request;
                        postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                    }

                    logoutUri += $"?returnTo={Uri.EscapeDataString(postLogoutUri)}";
                }

                context.Response.Redirect(logoutUri);
                context.HandleResponse();

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<KeycloakService>();

builder.Services.AddAuthorization();
builder.Services.AddHttpClient();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();