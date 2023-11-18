using BP.Common.Data;
using KeycloakUserSyncWorker;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddEntityFrameworkMySQL().AddDbContext<BPContext>(options =>
        {
            options.UseMySQL("server=localhost;port=3306;user=root;password=my-secret-pw;database=BP-scheme");
        });
        
        services.AddHostedService<Worker>();    
        services.AddScoped<UserSyncService>();

    })
    .Build();

host.Run();
