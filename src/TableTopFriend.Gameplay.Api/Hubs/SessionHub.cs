using MediatR;

namespace TableTopFriend.Gameplay.Api.Hubs;

public class SessionHub : HubBase
{
    public SessionHub(ISender sender) : base(sender) { }
}
