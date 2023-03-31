using DDDTableTopFriend.Application.Services.Authentication;
using DDDTableTopFriend.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DDDTableTopFriend.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpGet]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var authenticationResult = _authenticationService.Register(
            registerRequest.FirstName,
            registerRequest.LastName,
            registerRequest.Email,
            registerRequest.Password);

        return Ok(authenticationResult);
    }

    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var authenticationResult = _authenticationService.Login(loginRequest.Email, loginRequest.Password);
        return Ok(authenticationResult);
    }
}
