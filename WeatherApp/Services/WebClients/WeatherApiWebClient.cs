using System.Net.Http.Headers;
using System.Text.Json;

namespace WeatherApp.Services.WebClients;

using System.Net;

public class WeatherApiWebClient
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly ILogger<WeatherApiWebClient> _logger;
    private readonly string _baseUrl;

    public WeatherApiWebClient(ILogger<WeatherApiWebClient> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        string apiKey = _configuration["WeatherApiClientSetting:ApiKey"];
        var baseUrl = _configuration["WeatherApiClientSetting:BaseUrl"];
        _baseUrl = baseUrl;
        _httpClient.DefaultRequestHeaders.Add("key", apiKey);
    }

    public string GetCityCurrentWeather(string q)
    {
        var url = $"{_baseUrl}/current.json?q={q}";
        var response = _httpClient.GetAsync(url).Result;
        return response.Content.ReadAsStringAsync().Result;
    }

    public string CityLookup(string q)
    {
        var url = $"{_baseUrl}/search.json?q={q}";
        var response = _httpClient.GetAsync(url).Result;
        return response.Content.ReadAsStringAsync().Result;
    }
}