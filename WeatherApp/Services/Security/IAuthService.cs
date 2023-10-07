using WeatherApp.Models.Security;

namespace WeatherApp.Services.Security;

public interface IAuthService
{
    public string GenerateJwtToken(User user);
    public User? GetUserByEmail(string email);
    public bool VerifyPassword(User user, string password);
    public User? AddUser(string email, string password);
}