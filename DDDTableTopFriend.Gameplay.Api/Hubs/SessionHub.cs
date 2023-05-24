using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace DDDTableTopFriend.Gameplay.Api.Hubs;

public class SessionHub : Hub
{
    private readonly ISender _sender;
    public SessionHub(ISender sender)
    {
        _sender = sender;
    }
}
