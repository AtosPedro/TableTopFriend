using DDDTableTopFriend.Application.Campaigns.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Update;

public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, ErrorOr<CampaignResult>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ICachingService _cachingService;
    public UpdateCampaignCommandHandler(
        ICampaignRepository campaignRepository,
        IDateTimeProvider dateTimeProvider, ICachingService cachingService)
    {
        _campaignRepository = campaignRepository;
        _dateTimeProvider = dateTimeProvider;
        _cachingService = cachingService;
    }

    public async Task<ErrorOr<CampaignResult>> Handle(
        UpdateCampaignCommand request,
        CancellationToken cancellationToken)
    {
        var campaign = await _campaignRepository.GetById(
            CampaignId.Create(request.Id),
            cancellationToken);

        if(campaign is null)
            return Errors.Campaign.NotRegistered;

        var characterList = request.CharacterIds.Adapt<List<CharacterId>>();

        campaign.Update(
            request.Name,
            request.Description,
            characterList,
            _dateTimeProvider.UtcNow
        );

        await _campaignRepository.Update(campaign);
        await _cachingService.SetCacheValueAsync(campaign.Id.Value.ToString(), campaign);
        return campaign.Adapt<CampaignResult>();
    }
}
