using TableTopFriend.Application.Authentication.Commands.Register;
using TableTopFriend.Application.Authentication.Queries.Login;
using TableTopFriend.Contracts.Authentication;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TableTopFriend.Api.Controllers;

[Route("v1/api/auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    public AuthenticationController(ISender mediator) : base(mediator) { }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var command = registerRequest.Adapt<RegisterCommand>();
        var authenticationResult = await _sender.Send(command);

        return authenticationResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var query = loginRequest.Adapt<LoginQuery>();
        var authenticationResult = await _sender.Send(query);

        return authenticationResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors)
        );
    }
}
