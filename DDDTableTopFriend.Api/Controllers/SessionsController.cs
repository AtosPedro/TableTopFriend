using DDDTableTopFriend.Application.Campaigns.Commands.Delete;
using DDDTableTopFriend.Application.Sessions.Commands.Schedule;
using DDDTableTopFriend.Application.Sessions.Commands.Update;
using DDDTableTopFriend.Application.Sessions.Queries.Get;
using DDDTableTopFriend.Application.Sessions.Queries.GetAll;
using DDDTableTopFriend.Contracts.Skill;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDTableTopFriend.Api.Controllers;

[Route("v1/api/sessions")]
public class SessionsController : ApiController
{
    public SessionsController(ISender sender) : base(sender) { }


    [HttpGet("list/{campaignId}/{userId}")]
    public async Task<IActionResult> GetSessions(Guid campaignId, Guid userId)
    {
        var query = new GetAllSessionsQuery(userId, campaignId);
        var result = await _sender.Send(query);
        return result.Match(
            sessionResults => Ok(sessionResults),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSession(Guid id)
    {
        var query = new GetSessionQuery(id);
        var result = await _sender.Send(query);
        return result.Match(
            sessionResult => Ok(sessionResult),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> ScheduleSession(ScheduleSessionRequest request)
    {
        var command = request.Adapt<ScheduleSessionCommand>();
        var result = await _sender.Send(command);
        return result.Match(
            sessionResult => CreatedAtAction(nameof(GetSession), new { id = sessionResult.Id }, sessionResult),
            errors => Problem(errors)
        );
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSession(UpdateSessionRequest request)
    {
        var command = request.Adapt<UpdateSessionCommand>();
        var result = await _sender.Send(command);
        return result.Match(
            sessionResult => Ok(sessionResult),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSession(Guid id)
    {
        var command = new DeleteSessionCommand(id);
        var result = await _sender.Send(command);
        return result.Match(
            _ => NoContent(),
            errors => Problem(errors)
        );
    }
}
