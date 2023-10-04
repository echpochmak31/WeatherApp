using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WeatherApp.DataAccess;
using WeatherApp.Models.Security;

namespace WeatherApp.Services.Security;

public class AuthService : IAuthService
{
    
    private readonly WeatherContext _dbContext;
    private readonly IConfiguration _configuration;

    public AuthService(WeatherContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["WEATHER_APP_SECRET_KEY"])),
                SecurityAlgorithms.HmacSha256Signature)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenString;
    }

    public User? GetUserByEmail(string email)
    {
        return _dbContext.Users.FirstOrDefault(u => u != null && u.Email == email);
    }

    public bool VerifyPassword(User user, string password)
    {
        return user.Password == HashPassword(password);
    }

    public User? AddUser(string email, string password)
    {
        var user = new User(email, HashPassword(password));
         _dbContext.Users.Add(user);
         _dbContext.SaveChanges();
         return user;
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }
}