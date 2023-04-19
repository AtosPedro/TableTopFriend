using DDDTableTopFriend.Application.Users.Commands.Delete;
using DDDTableTopFriend.Application.Users.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDTableTopFriend.Api.Controllers;

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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var command = new DeleteUserCommand(id);
        var result = await _sender.Send(command);
        return result.Match(
            userResult => Ok(userResult),
            errors => Problem(errors)
        );
    }
}
