namespace KeycloakUserSyncWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var userSyncService = scope.ServiceProvider.GetRequiredService<UserSyncService>();
                    userSyncService.SyncUsers();
                }
                _logger.LogInformation("Users synced : {time}", DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during synchronization");
            }
            await Task.Delay(TimeSpan.FromSeconds(20), stoppingToken);
        }
    }
}
