using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lombok.NET;

namespace WeatherApp.Models;

[Table("location_weather")]
public class LocationWeather
{
    [Key, Column("latitude")] public decimal Latitude { get; set; }

    [Key, Column("longitude")] public decimal Longitude { get; set; }

    [Column("name")] public string Name { get; set; }

    [Column("region")] public string Region { get; set; }

    [Column("country")] public string Country { get; set; }

    [Column("last_updated")] public DateTimeOffset LastUpdated { get; set; }

    [Column("temp_c")] public decimal TemperatureСelsius { get; set; }

    [Column("feels_like_c")] public decimal FeelsLikeСelsius { get; set; }

    [Column("condition_text")] public string ConditionText { get; set; }

    [Column("condition_image_url")] public string ConditionImageUrl { get; set; }

    [Column("condition_code")] public int ConditionCode { get; set; }

    public decimal LocationGroupItemLatitude { get; set; }
    public decimal LocationGroupItemLongitude { get; set; }

    [ForeignKey("LocationGroupItemLatitude, LocationGroupItemLongitude")]
    public LocationGroupItem LocationGroupItem { get; set; }
}