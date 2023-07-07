using MediatR;
using Microsoft.AspNetCore.SignalR;
using TableTopFriend.Contracts.Gameplay.Api.ChatHub;

namespace TableTopFriend.Gameplay.Api.Hubs;

public class ChatHub : Hub<IChatHub>
{
    private readonly ISender _sender;
    public ChatHub(ISender sender)
    {
        _sender = sender;
    }
}
