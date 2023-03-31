using DDDTableTopFriend.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DDDTableTopFriend.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthenticationController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        return Ok();
    }

    public async Task<IActionResult> Register(LoginRequest loginRequest)
    {
        return Ok();
    }
}
