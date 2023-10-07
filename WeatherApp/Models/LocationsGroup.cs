using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeatherApp.Models.Security;

namespace WeatherApp.Models;

[Table("location_group")]
public class LocationGroup
{
    [Key] public int Id { get; set; }

    [Required] public string UserEmail { get; set; }

    [Required] public string GroupName { get; set; }

    public List<LocationGroupItem> Items { get; set; }

    [ForeignKey(nameof(User))] public long UserId { get; set; }

    public User User { get; set; }
}