using TableTopFriend.Application.Characters.Commands.Create;
using TableTopFriend.Application.Characters.Commands.Delete;
using TableTopFriend.Application.Characters.Commands.Update;
using TableTopFriend.Application.Characters.Queries.Get;
using TableTopFriend.Application.Characters.Queries.GetAll;
using TableTopFriend.Contracts.Api.Character;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TableTopFriend.Api.Controllers;

[Route("v1/api/characters")]
public class CharactersController : ApiController
{
    public CharactersController(ISender sender) : base(sender) { }

    [HttpGet("list/{userId}")]
    public async Task<IActionResult> GetCharacters(Guid userId)
    {
        var query = new GetAllCharactersQuery(userId);
        var result = await _sender.Send(query);
        return result.Match(
            audioEffectResult => Ok(audioEffectResult),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCharacter(Guid id)
    {
        var query = new GetCharacterQuery(id);
        var result = await _sender.Send(query);
        return result.Match(
            audioEffectResult => Ok(audioEffectResult),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> CreateCharacter(CreateCharacterRequest request)
    {
        var command = request.Adapt<CreateCharacterCommand>();
        var result = await _sender.Send(command);
        return result.Match(
            characterResult => CreatedAtAction(nameof(GetCharacter), new { id = characterResult.Id }, characterResult),
            errors => Problem(errors)
        );
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCharacter(UpdateCharacterRequest request)
    {
        var command = request.Adapt<UpdateCharacterCommand>();
        var result = await _sender.Send(command);
        return result.Match(
            audioEffectResult => Ok(audioEffectResult),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCharacter(Guid id)
    {
        var command = new DeleteCharacterCommand(id);
        var result = await _sender.Send(command);
        return result.Match(
            _ => NoContent(),
            errors => Problem(errors)
        );
    }
}
