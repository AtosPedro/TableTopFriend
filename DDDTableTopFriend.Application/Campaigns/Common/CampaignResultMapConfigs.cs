using DDDTableTopFriend.Domain.AggregateCampaign;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using Mapster;
using DDDTableTopFriend.Application.Campaigns.Commands.Create;
using DDDTableTopFriend.Application.Campaigns.Commands.Update;

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

        config.NewConfig<IEnumerable<Campaign>, IEnumerable<CampaignResult>>()
            .Map(dest => dest, src => src.Select(x => x.Adapt<CampaignResult>()))
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
