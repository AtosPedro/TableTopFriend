using Mapster;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using TableTopFriend.Application.Characters.Commands.CastSkill;
using TableTopFriend.Contracts.Gameplay.Api.Session;

namespace TableTopFriend.Gameplay.Api.Hubs;

public class SessionHub : HubBase
{
    public SessionHub(ISender sender) : base(sender) { }

    public async Task CharacterCastedSpellMessage(CharacterCastedSpellRequest request)
    {
        var command = request.Adapt<CharacterCastedSkillCommand>();
        var result = await Sender.Send(command);
        await result.MatchAsync(
            async result => await Clients.All.SendAsync("", result),
            async error => await Clients.All.SendAsync("", error));
    }
}
