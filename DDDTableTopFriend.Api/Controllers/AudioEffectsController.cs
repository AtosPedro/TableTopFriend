using MediatR;
using Microsoft.AspNetCore.Components;

namespace DDDTableTopFriend.Api.Controllers;

[Route("v1/api/audio-effects")]
public class AudioEffectsController : ApiController
{
    public AudioEffectsController(ISender sender) : base(sender) { }
}
