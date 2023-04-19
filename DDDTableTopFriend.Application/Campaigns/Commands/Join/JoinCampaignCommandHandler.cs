using DDDTableTopFriend.Application.Campaigns.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateCampaign.Events;
using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Join;

public class JoinCampaignCommandHandler : IRequestHandler<JoinCampaignCommand, ErrorOr<CampaignJoinedResult>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ICharacterRepository _characterRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICachingService _cachingService;
    public JoinCampaignCommandHandler(
        ICampaignRepository campaignRepository,
        ICharacterRepository characterRepository,
        IDateTimeProvider dateTimeProvider,
        ICachingService cachingService)
    {
        _campaignRepository = campaignRepository;
        _characterRepository = characterRepository;
        _dateTimeProvider = dateTimeProvider;
        _cachingService = cachingService;
    }

    public async Task<ErrorOr<CampaignJoinedResult>> Handle(
        JoinCampaignCommand request,
        CancellationToken cancellationToken)
    {
        var campaign = await _campaignRepository.GetById(
            CampaignId.Create(request.Id),
            cancellationToken);

        if (campaign is null)
            return Errors.Campaign.NotRegistered;

        var character = await _characterRepository.GetById(
            CharacterId.Create(request.Id),
            cancellationToken);

        if (character is null)
            return Errors.Character.NotRegistered;

        campaign.AddCharacterId(
            CharacterId.Create(request.Id),
            _dateTimeProvider.UtcNow
        );

        await _campaignRepository.Update(campaign);
        var result = campaign.Adapt<CampaignJoinedResult>();
        await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
        return result;
    }
}
