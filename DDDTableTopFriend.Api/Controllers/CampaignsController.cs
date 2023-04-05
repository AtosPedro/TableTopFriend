using DDDTableTopFriend.Contracts.Campaign;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDTableTopFriend.Api.Controllers;

[Route("v1/api/campaigns")]
public class CampaignsController : ApiController
{
    public CampaignsController(ISender mediator) : base(mediator)
    {
    }

    public async Task<IActionResult> CreateCampaign(CreateCampaignRequest request)
    {
        var command = request.Adapt<CreateCampaignCommand>();
        var result = await _mediator.Send(command);
        return result.Match(
            campaignResult => Ok(campaignResult),
            errors => Problem(errors)
        );
    }
}
