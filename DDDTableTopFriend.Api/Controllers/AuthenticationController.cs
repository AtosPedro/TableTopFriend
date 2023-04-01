using DDDTableTopFriend.Application.Authentication.Login.Queries;
using DDDTableTopFriend.Application.Authentication.Register.Commands;
using DDDTableTopFriend.Contracts.Authentication;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDTableTopFriend.Api.Controllers;

[Route("v1/api/auth")]
public class AuthenticationController : ApiController
{
    public AuthenticationController(ISender mediator) : base(mediator) { }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var command = registerRequest.Adapt<RegisterCommand>();
        var authenticationResult = await _mediator.Send(command);

        return authenticationResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var query = loginRequest.Adapt<LoginQuery>();
        var authenticationResult = await _mediator.Send(query);

        return authenticationResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors)
        );
    }
}
