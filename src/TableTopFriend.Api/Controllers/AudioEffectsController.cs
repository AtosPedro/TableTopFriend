using TableTopFriend.Application.AudioEffects.Commands.Create;
using TableTopFriend.Application.AudioEffects.Commands.Delete;
using TableTopFriend.Application.AudioEffects.Commands.Update;
using TableTopFriend.Application.AudioEffects.Queries.Get;
using TableTopFriend.Application.AudioEffects.Queries.GetAll;
using TableTopFriend.Contracts.Campaign;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TableTopFriend.Api.Controllers;

[Route("v1/api/audio-effects")]
public class AudioEffectsController : ApiController
{
    public AudioEffectsController(ISender sender) : base(sender) { }

    [HttpGet("list/{userId}")]
    public async Task<IActionResult> GetAudioEffects(Guid userId)
    {
        var query = new GetAllAudioEffectsQuery(userId);
        var result = await _sender.Send(query);
        return result.Match(
            audioEffectResult => Ok(audioEffectResult),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAudioEffect(Guid id)
    {
        var query = new GetAudioEffectQuery(id);
        var result = await _sender.Send(query);
        return result.Match(
            audioEffectResult => Ok(audioEffectResult),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> CreateAudioEffect(CreateAudioEffectRequest request)
    {
        var command = request.Adapt<CreateAudioEffectCommand>();
        var result = await _sender.Send(command);
        return result.Match(
            audioEffectResult => CreatedAtAction(nameof(GetAudioEffect), new { id = audioEffectResult.Id }, audioEffectResult),
            errors => Problem(errors)
        );
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAudioEffect(UpdateAudioEffectRequest request)
    {
        var command = request.Adapt<UpdateAudioEffectCommand>();
        var result = await _sender.Send(command);
        return result.Match(
            audioEffectResult => Ok(audioEffectResult),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAudioEffect(Guid id)
    {
        var command = new DeleteAudioEffectCommand(id);
        var result = await _sender.Send(command);
        return result.Match(
            _ => NoContent(),
            errors => Problem(errors)
        );
    }
}
