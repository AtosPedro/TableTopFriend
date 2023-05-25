using TableTopFriend.Domain.AggregateSession;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using Mapster;

namespace TableTopFriend.Application.Sessions.Common;

public class SessionResultMapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Session, SessionResult>()
            .Map(dest => dest.Id, src => src.GetId().Value)
            .Map(dest => dest.UserId, src => src.UserId.Value)
            .Map(dest => dest.Name, src => src.Name.Value)
            .Map(dest => dest.Description, src => src.Description.Value)
            .Map(dest => dest.CampaignId, src => src.CampaignId.Value)
            .Map(dest => dest.CharacterIds, src => src.CharacterIds.Select(x => x.Value))
            .Map(dest => dest.AudioEffectIds, src => src.AudioEffectIds.Select(x => x.Value))
            .MapToConstructor(true);
    }
}
