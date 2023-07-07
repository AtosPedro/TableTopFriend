using MediatR;
using Microsoft.AspNetCore.SignalR;
using TableTopFriend.Contracts.Gameplay.Api.SkillHub;

namespace TableTopFriend.Gameplay.Api.Hubs;

public class SkillHub : Hub<ISkillHub>
{
    private readonly ISender _sender;
    public SkillHub(ISender sender)
    {
        _sender = sender;
    }
}
