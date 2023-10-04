using Microsoft.EntityFrameworkCore;
using WeatherApp.Models.Security;

namespace WeatherApp.DataAccess;

public class WeatherContext : DbContext
{
    public DbSet<User?> Users { get; set; }

    public WeatherContext(DbContextOptions options) : base(options)
    {
        
    }
}