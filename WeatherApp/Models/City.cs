using Lombok.NET;

namespace WeatherApp.Models;

// [RequiredArgsConstructor(MemberType = MemberType.Property, AccessTypes = AccessTypes.Public)]
public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
};