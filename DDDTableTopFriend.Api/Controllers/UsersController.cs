using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDTableTopFriend.Api.Controllers;

[Route("v1/api/users")]
public class UsersController : ApiController
{
    public UsersController(ISender mediator) : base(mediator)
    {
    }
}
