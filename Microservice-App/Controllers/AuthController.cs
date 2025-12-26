using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microservice_App.Auth;
using Microsoft.Extensions.Options;

namespace Microservice_App.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : Controller
{
    private readonly JwtOptions _jwtOptions;

    public AuthController(IOptions<JwtOptions>  jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var claims = new List<Claim>();
        
        if (request.Username == "admin" && request.Password == "admin")
        {
            claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
        }
        else
        {
            claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, "User"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtOptions.Key));

        var credentials = new SigningCredentials(
            key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return Ok(new
        {
            access_token = tokenString
        });
    }

    [Authorize]
    [HttpGet("secure-ping")]
    public IActionResult PingWithAuth()
    {
        return Ok("Authorized Pong");
    }

    [HttpGet("ping")]
    public IActionResult SimplePing()
    {
        return Ok("Just a Pong");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin-only")]
    public IActionResult AdminPanel()
    {
        return Ok("Admin Panel is working!");
    }
}