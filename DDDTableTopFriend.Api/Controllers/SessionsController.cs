using MediatR;
using Microsoft.AspNetCore.Components;

namespace DDDTableTopFriend.Api.Controllers;

[Route("v1/api/sessions")]
public class SessionsController : ApiController
{
    public SessionsController(ISender sender) : base(sender) { }
}
