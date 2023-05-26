using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace TableTopFriend.Gameplay.Api.Hubs;

public class ChatHub : HubBase
{
    public ChatHub(ISender sender) : base(sender) { }

    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
