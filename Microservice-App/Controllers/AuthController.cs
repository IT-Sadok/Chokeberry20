using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microservice_App.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : Controller
{
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
}