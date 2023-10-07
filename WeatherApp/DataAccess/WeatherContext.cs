using Microsoft.EntityFrameworkCore;
using WeatherApp.Models;
using WeatherApp.Models.Security;

namespace WeatherApp.DataAccess;

public class WeatherContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<LocationGroup> LocationGroups { get; set; }
    public DbSet<LocationGroupItem> LocationGroupItems { get; set; }
    public DbSet<LocationWeather> LocationWeathers { get; set; }

    public WeatherContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LocationGroupItem>()
            .HasKey(item => new { item.Latitude, item.Longitude });

        modelBuilder.Entity<LocationWeather>()
            .HasKey(weather => new { weather.LocationGroupItemLatitude, weather.LocationGroupItemLongitude });
    }
}