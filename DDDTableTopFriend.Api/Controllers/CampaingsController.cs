using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDTableTopFriend.Api.Controllers;

[Route("v1/api/campaigns")]
public class CampaignsController : ApiController
{
    public CampaignsController(ISender mediator) : base(mediator){}

    [HttpGet]
    public IActionResult ListCampaigns(){
        return Ok();
    }
}
