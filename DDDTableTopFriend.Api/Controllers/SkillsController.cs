using DDDTableTopFriend.Application.Skills.Commands.Create;
using DDDTableTopFriend.Application.Skills.Commands.Delete;
using DDDTableTopFriend.Application.Skills.Commands.Update;
using DDDTableTopFriend.Application.Skills.Queries.Get;
using DDDTableTopFriend.Application.Skills.Queries.GetAll;
using DDDTableTopFriend.Contracts.Skill;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDTableTopFriend.Api.Controllers;

[Route("v1/api/skills")]
public class SkillsController : ApiController
{
    public SkillsController(ISender sender) : base(sender) { }

    [HttpGet("list/{userId}")]
    public async Task<IActionResult> GetSkills(Guid userId)
    {
        var query = new GetAllSkillsQuery(userId);
        var result = await _sender.Send(query);
        return result.Match(
            userResult => Ok(userResult),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSkill(Guid id)
    {
        var query = new GetSkillQuery(id);
        var result = await _sender.Send(query);
        return result.Match(
            userResult => Ok(userResult),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> CreateSkill(CreateSkillRequest request)
    {
        var command = request.Adapt<CreateSkillCommand>();
        var result = await _sender.Send(command);
        return result.Match(
            skillResult => CreatedAtAction(nameof(GetSkill), new { id = skillResult.Id }, skillResult),
            errors => Problem(errors)
        );
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSkill(UpdateSkillRequest request)
    {
        var command = request.Adapt<UpdateSkillCommand>();
        var result = await _sender.Send(command);
        return result.Match(
            statusResult => Ok(statusResult),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSkill(Guid id)
    {
        var command = new DeleteSkillCommand(id);
        var result = await _sender.Send(command);
        return result.Match(
            userResult => Ok(userResult),
            errors => Problem(errors)
        );
    }
}
