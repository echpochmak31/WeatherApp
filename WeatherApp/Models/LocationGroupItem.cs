using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherApp.Models;

[Table("location_group_item")]
public class LocationGroupItem
{
    [Key, ForeignKey(nameof(LocationWeather))]
    [Column(Order = 0)]
    public decimal Latitude { get; set; }

    [Key, ForeignKey(nameof(LocationWeather))]
    [Column(Order = 1)]
    public decimal Longitude { get; set; }

    public int LocationGroupId { get; set; }

    [ForeignKey(nameof(LocationGroupId))]
    public LocationGroup LocationGroup { get; set; }
}