using System.Net.Http.Headers;

namespace WeatherApp.Services.WebClients;

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

        string apiKey = _configuration["WEATHER_APP_API_KEY"];
        var baseUrl = _configuration["WeatherApiClientSetting:BaseUrl"];
        _baseUrl = baseUrl;
        _httpClient.DefaultRequestHeaders.Add("key", apiKey);
    }

    public async Task<string> GetCurrentWeatherAsync(string q)
    {
        var url = $"{_baseUrl}/current.json?q={q}";
        var response = await _httpClient.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> LocationLookup(string q)
    {
        var url = $"{_baseUrl}/search.json?q={q}";
        var response = await _httpClient.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
}