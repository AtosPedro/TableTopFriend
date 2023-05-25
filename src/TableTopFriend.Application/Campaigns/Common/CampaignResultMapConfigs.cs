using TableTopFriend.Domain.AggregateCampaign;
using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.AggregateSession.ValueObjects;
using TableTopFriend.Domain.AggregateCampaign.ValueObjects;
using TableTopFriend.Application.Campaigns.Commands.Create;
using TableTopFriend.Application.Campaigns.Commands.Update;
using Mapster;

namespace TableTopFriend.Application.Campaigns.Common;

public class CampaignResultMapConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Campaign, CampaignResult>()
            .Map(dest => dest.Id, src => src.GetId().Value)
            .Map(dest => dest.UserId, src => src.UserId.Value)
            .Map(dest => dest.Name, src => src.Name.Value)
            .Map(dest => dest.Description, src => src.Description.Value)
            .Map(dest => dest.CharacterIds, src => src.CharacterIds.Select(x => x.Value))
            .Map(dest => dest.SessionIds, src => src.SessionIds.Select(x => x.Value))
            .MapToConstructor(true);

        config.NewConfig<IEnumerable<Campaign>, IEnumerable<CampaignResult>>()
            .Map(dest => dest, src => src.Select(x => x.Adapt<CampaignResult>()))
            .MapToConstructor(true);

        config.NewConfig<CreateCampaignCommand, Campaign>()
            .Map(dest => dest.UserId, src => UserId.Create(src.UserId))
            .Map(dest => dest.CharacterIds, src => src.CharacterIds.ConvertAll(x => CharacterId.Create(x)))
            .MapToConstructor(true);

        config.NewConfig<UpdateCampaignCommand, Campaign>()
            .Map(dest => dest.Id, src => CampaignId.Create(src.Id))
            .Map(dest => dest.UserId, src => UserId.Create(src.UserId))
            .Map(dest => dest.CharacterIds, src => src.CharacterIds.ConvertAll(x => CharacterId.Create(x)))
            .MapToConstructor(true);

        config.NewConfig<IEnumerable<Guid>, IEnumerable<CharacterId>>()
           .Map(dest => dest, src => src.Select(x => CharacterId.Create(x)))
           .MapToConstructor(true);

        config.NewConfig<IEnumerable<Guid>, IEnumerable<SessionId>>()
           .Map(dest => dest, src => src.Select(x => SessionId.Create(x)))
           .MapToConstructor(true);
    }
}
