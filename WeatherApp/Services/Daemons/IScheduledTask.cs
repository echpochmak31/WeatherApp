namespace WeatherApp.Services.Daemons;

public interface IScheduledTask
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}