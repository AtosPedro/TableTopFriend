using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDTableTopFriend.Api.Controllers;

[Route("v1/api/sessions")]
public class SessionsController : ApiController
{
    public SessionsController(ISender sender) : base(sender) { }

    [HttpGet("list/{userId}")]
    public async Task<IActionResult> GetSessions(Guid userId)
    {
        var query = new GetAllSessionsQuery(userId);
        var result = await _sender.Send(query);
        return result.Match(
            userResult => Ok(userResult),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSession(Guid id)
    {
        var query = new GetSessionQuery(id);
        var result = await _sender.Send(query);
        return result.Match(
            userResult => Ok(userResult),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> ScheduleSession(ScheduleSessionRequest request)
    {
        var command = request.Adapt<ScheduleSessionCommand>();
        var result = await _sender.Send(command);
        return result.Match(
            skillResult => CreatedAtAction(nameof(GetSkill), new { id = skillResult.Id }, skillResult),
            errors => Problem(errors)
        );
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSession(UpdateSessionRequest request)
    {
        var command = request.Adapt<UpdateSessionCommand>();
        var result = await _sender.Send(command);
        return result.Match(
            statusResult => Ok(statusResult),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSession(Guid id)
    {
        var command = new DeleteSessionCommand(id);
        var result = await _sender.Send(command);
        return result.Match(
            userResult => Ok(userResult),
            errors => Problem(errors)
        );
    }
}
