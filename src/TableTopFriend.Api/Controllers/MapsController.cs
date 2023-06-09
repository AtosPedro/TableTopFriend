using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TableTopFriend.Application.Map.Commands.Create;
using TableTopFriend.Application.Map.Commands.Delete;
using TableTopFriend.Application.Map.Commands.Update;
using TableTopFriend.Application.Map.Queries.Get;
using TableTopFriend.Application.Map.Queries.GetAll;
using TableTopFriend.Contracts.Api.Map;

namespace TableTopFriend.Api.Controllers;

[Route("v1/api/maps")]
public class MapsController : ApiController
{
    public MapsController(ISender sender) : base(sender) { }

   [HttpGet("list/{userId}")]
    public async Task<IActionResult> GetMaps(Guid userId)
    {
        var query = new GetAllMapsQuery(userId);
        var result = await _sender.Send(query);
        return result.Match(
            audioEffectResult => Ok(audioEffectResult),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMap(Guid id)
    {
        var query = new GetMapQuery(id);
        var result = await _sender.Send(query);
        return result.Match(
            audioEffectResult => Ok(audioEffectResult),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> CreateMap(CreateMapRequest request)
    {
        var command = request.Adapt<CreateMapCommand>();
        var result = await _sender.Send(command);
        return result.Match(
            mapResult => CreatedAtAction(nameof(GetMap), new { id = mapResult.Id }, mapResult),
            errors => Problem(errors)
        );
    }

    [HttpPut]
    public async Task<IActionResult> UpdateMap(UpdateMapRequest request)
    {
        var command = request.Adapt<UpdateMapCommand>();
        var result = await _sender.Send(command);
        return result.Match(
            mapResult => Ok(mapResult),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMap(Guid id)
    {
        var command = new DeleteMapCommand(id);
        var result = await _sender.Send(command);
        return result.Match(
            _ => NoContent(),
            errors => Problem(errors)
        );
    }
}
