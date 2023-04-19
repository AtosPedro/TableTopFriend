using DDDTableTopFriend.Application.Campaigns.Commands.Create;
using DDDTableTopFriend.Application.Campaigns.Commands.Delete;
using DDDTableTopFriend.Application.Campaigns.Commands.Join;
using DDDTableTopFriend.Application.Campaigns.Commands.Update;
using DDDTableTopFriend.Application.Campaigns.Get.Queries;
using DDDTableTopFriend.Application.Campaigns.GetAll.Queries;
using DDDTableTopFriend.Contracts.Campaign;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDTableTopFriend.Api.Controllers;

[Route("v1/api/campaigns")]
public class CampaignsController : ApiController
{
    public CampaignsController(ISender sender) : base(sender) { }

    [HttpGet("list/{userId}")]
    public async Task<IActionResult> GetCampaigns(Guid userId)
    {
        var query = new GetAllCampaignQuery(userId);
        var result = await _sender.Send(query);
        return result.Match(
            campaignResult => Ok(campaignResult),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCampaign(Guid id)
    {
        var query = new GetCampaignQuery(id);
        var result = await _sender.Send(query);
        return result.Match(
            campaignResult => Ok(campaignResult),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> CreateCampaign(CreateCampaignRequest request)
    {
        var command = request.Adapt<CreateCampaignCommand>();
        var result = await _sender.Send(command);
        return result.Match(
            campaignResult => CreatedAtAction(nameof(GetCampaign), new { id = campaignResult.Id }, campaignResult),
            errors => Problem(errors)
        );
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCampaign(UpdateCampaignRequest request)
    {
        var command = request.Adapt<UpdateCampaignCommand>();
        var result = await _sender.Send(command);
        return result.Match(
            campaignResult => Ok(campaignResult),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCampaign(Guid id)
    {
        var result = await _sender.Send(new DeleteCampaignCommand(id));
        return result.Match(
            _ => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpPatch]
    public async Task<IActionResult> JoinCampaign(JoinCampaignRequest request)
    {
        var command = request.Adapt<JoinCampaignCommand>();
        var result = await _sender.Send(command);
        return result.Match(
            campaignJoinedResult => Ok(campaignJoinedResult),
            errors => Problem(errors)
        );
    }
}
