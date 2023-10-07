using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lombok.NET;

namespace WeatherApp.Models;

// [RequiredArgsConstructor(MemberType = MemberType.Property, AccessTypes = AccessTypes.Public)]
public class Location
{
    public long LocationId { get; set; }
    public string Name { get; set; }
    public string Region { get; set; }

    public string Country { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}