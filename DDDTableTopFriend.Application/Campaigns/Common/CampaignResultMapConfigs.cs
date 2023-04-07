using DDDTableTopFriend.Application.Campaigns.Create.Commands;
using DDDTableTopFriend.Domain.Campaign;
using DDDTableTopFriend.Domain.Character.ValueObjects;
using DDDTableTopFriend.Domain.Users.ValueObjects;
using DDDTableTopFriend.Domain.Session.ValueObjects;
using Mapster;
using DDDTableTopFriend.Domain.Campaign.ValueObjects;

namespace DDDTableTopFriend.Application.Campaigns.Common;

public class CampaignResultMapConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Campaign, CampaignResult>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.CharacterIds, src => src.CharacterIds.Select(x => x.Value))
            .Map(dest => dest.SessionIds, src => src.SessionIds.Select(x => x.Value))
            .MapToConstructor(true);

        config.NewConfig<CreateCampaignCommand, Campaign>()
            .Map(dest => dest.UserId, src => UserId.Create(src.UserId))
            .Map(dest => dest.CharacterIds, src => src.CharacterIds.ConvertAll(x => CharacterId.Create(x)))
            .Map(dest => dest.SessionIds, src => src.SessionIds.ConvertAll(x => SessionId.Create(x)))
            .MapToConstructor(true);

        config.NewConfig<UpdateCampaignCommand, Campaign>()
            .Map(dest => dest.Id, src => CampaignId.Create(src.Id))
            .Map(dest => dest.UserId, src => UserId.Create(src.UserId))
            .Map(dest => dest.CharacterIds, src => src.CharacterIds.ConvertAll(x => CharacterId.Create(x)))
            .Map(dest => dest.SessionIds, src => src.SessionIds.ConvertAll(x => SessionId.Create(x)));
    }
}
