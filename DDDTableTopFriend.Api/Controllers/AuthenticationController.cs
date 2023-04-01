using DDDTableTopFriend.Application.Services.Authentication;
using DDDTableTopFriend.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DDDTableTopFriend.Api.Controllers;

[ApiController]
[Route("v1/api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest registerRequest)
    {
        var authenticationResult = _authenticationService.Register(
            registerRequest.FirstName,
            registerRequest.LastName,
            registerRequest.Email,
            registerRequest.Password);

        return Ok(authenticationResult);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest loginRequest)
    {
        var authenticationResult = _authenticationService.Login(loginRequest.Email, loginRequest.Password);
        return Ok(authenticationResult);
    }
}
