using TableTopFriend.Application.Campaigns.Common;
using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Domain.AggregateCampaign.ValueObjects;
using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using TableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace TableTopFriend.Application.Campaigns.Commands.Join;

public class JoinCampaignCommandHandler : IRequestHandler<JoinCampaignCommand, ErrorOr<CampaignJoinedResult>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ICharacterRepository _characterRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICachingService _cachingService;
    private readonly IUnitOfWork _unitOfWork;
    public JoinCampaignCommandHandler(
        ICampaignRepository campaignRepository,
        ICharacterRepository characterRepository,
        IDateTimeProvider dateTimeProvider,
        ICachingService cachingService,
        IUnitOfWork unitOfWork)
    {
        _campaignRepository = campaignRepository;
        _characterRepository = characterRepository;
        _dateTimeProvider = dateTimeProvider;
        _cachingService = cachingService;
        _unitOfWork = unitOfWork;
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

        return await _unitOfWork.Execute(async _ =>
        {
            await _campaignRepository.Update(campaign);
            var result = campaign.Adapt<CampaignJoinedResult>();
            await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
            return result;
        },
        cancellationToken);
    }
}
