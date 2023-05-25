using TableTopFriend.Application.Statuses.Commands.Create;
using TableTopFriend.Application.Statuses.Commands.Delete;
using TableTopFriend.Application.Statuses.Commands.Update;
using TableTopFriend.Application.Statuses.Queries.Get;
using TableTopFriend.Application.Statuses.Queries.GetAll;
using TableTopFriend.Contracts.Status;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TableTopFriend.Api.Controllers;

[Route("v1/api/statuses")]
public class StatusesController : ApiController
{
    public StatusesController(ISender sender) : base(sender) { }

    [HttpGet("list/{userId}")]
    public async Task<IActionResult> GetStatuses(Guid userId)
    {
        var query = new GetAllStatusQuery(userId);
        var result = await _sender.Send(query);
        return result.Match(
            statusResult => Ok(statusResult),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStatus(Guid id)
    {
        var query = new GetStatusQuery(id);
        var result = await _sender.Send(query);
        return result.Match(
            statusResult => Ok(statusResult),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> CreateStatus(CreateStatusRequest request)
    {
        var command = request.Adapt<CreateStatusCommand>();
        var result = await _sender.Send(command);
        return result.Match(
            statusResult => CreatedAtAction(nameof(GetStatus), new { id = statusResult.Id }, statusResult),
            errors => Problem(errors)
        );
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStatus(UpdateStatusRequest request)
    {
        var command = request.Adapt<UpdateStatusCommand>();
        var result = await _sender.Send(command);
        return result.Match(
            statusResult => Ok(statusResult),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStatus(Guid id)
    {
        var command = new DeleteStatusCommand(id);
        var result = await _sender.Send(command);
        return result.Match(
            _ => NoContent(),
            errors => Problem(errors)
        );
    }
}
