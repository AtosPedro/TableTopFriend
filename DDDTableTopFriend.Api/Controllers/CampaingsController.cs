using DDDTableTopFriend.Application.Campaigns.Create.Commands;
using DDDTableTopFriend.Contracts.Campaign;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDTableTopFriend.Api.Controllers;

[Route("v1/api/campaigns")]
public class CampaignsController : ApiController
{
    public CampaignsController(ISender mediator) : base(mediator){}

    [HttpPost]

    public async Task<IActionResult> CreateCampaign(CreateCampaignRequest request)
    {
        var command = request.Adapt<CreateCampaignCommand>();
        var campaignResult = await _mediator.Send(command);
        return campaignResult.Match(
            campaResult => Ok(campaResult),
            errors => Problem(errors)
        );
    }
}
