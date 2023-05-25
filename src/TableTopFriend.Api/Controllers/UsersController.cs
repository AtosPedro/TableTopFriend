using TableTopFriend.Application.Users.Commands.Delete;
using TableTopFriend.Application.Users.Commands.Validate;
using TableTopFriend.Application.Users.Queries.Get;
using TableTopFriend.Contracts.User;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TableTopFriend.Api.Controllers;

[Route("v1/api/users")]
public class UsersController : ApiController
{
    public UsersController(ISender sender) : base(sender) { }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var query = new GetUserQuery(id);
        var result = await _sender.Send(query);
        return result.Match(
            userResult => Ok(userResult),
            errors => Problem(errors)
        );
    }

    [HttpPost("validate/{id}")]
    public async Task<IActionResult> ValidateUser(Guid id)
    {
        var command = new ValidateUserCommand(id);
        var result = await _sender.Send(command);
        return result.Match(
            userResult => Ok(userResult),
            errors => Problem(errors)
        );
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
    {
        var command = request.Adapt<UpdateUserCommand>();
        var result = await _sender.Send(command);
        return result.Match(
            userResult => Ok(userResult),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var command = new DeleteUserCommand(id);
        var result = await _sender.Send(command);
        return result.Match(
            _ => NoContent(),
            errors => Problem(errors)
        );
    }
}
