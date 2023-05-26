using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace TableTopFriend.Gameplay.Api.Hubs;

public class HubBase : Hub
{
    protected readonly ISender Sender;
    public HubBase(ISender sender)
    {
        Sender = sender;
    }
}
