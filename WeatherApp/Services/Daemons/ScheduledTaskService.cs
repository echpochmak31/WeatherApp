namespace WeatherApp.Services.Daemons;

public class ScheduledTaskService : BackgroundService
{
    
    private readonly IServiceProvider _serviceProvider;

    public ScheduledTaskService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var tasks = scope.ServiceProvider.GetServices<IScheduledTask>();

            foreach (var task in tasks)
            {
                await task.ExecuteAsync(stoppingToken);
            }
        }
    }
}