using MediatR;
using Microsoft.AspNetCore.SignalR;
using TableTopFriend.Contracts.Gameplay.Api.MapHub;

namespace TableTopFriend.Gameplay.Api.Hubs;

public class MapHub : Hub<IMapHub>
{
    private readonly ISender _sender;
    public MapHub(ISender sender)
    {
        _sender = sender;
    }
}
