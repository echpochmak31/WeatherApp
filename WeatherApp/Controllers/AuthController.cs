using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models.Requests;
using WeatherApp.Models.Security;
using WeatherApp.Services.Security;

namespace WeatherApp.Controllers;


[ApiController]
[Route("weather/auth")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _authService.GetUserByEmail(request.Email);

        if (user is null || !_authService.VerifyPassword(user, request.Password))
        {
            return Unauthorized(new { message = "Invalid user credentials" });
        }

        var token = _authService.GenerateJwtToken(user);

        return Ok(new { token });
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        var existingUser = _authService.GetUserByEmail(request.Email);
        if (existingUser is not null)
        {
            return BadRequest(new { message = $"User with email {request.Email} already exists" });
        }

        var user = _authService.AddUser(request.Email, request.Password);
        if (user != null)
        {
            var token = _authService.GenerateJwtToken(user);
            return Ok(new { token });
        }

        return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add user");
    }

}