using TableTopFriend.Application.Campaigns.Common;
using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Domain.AggregateCampaign.ValueObjects;
using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using TableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace TableTopFriend.Application.Campaigns.Commands.Update;

public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, ErrorOr<CampaignResult>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICachingService _cachingService;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateCampaignCommandHandler(
        ICampaignRepository campaignRepository,
        IDateTimeProvider dateTimeProvider,
        ICachingService cachingService,
        IUnitOfWork unitOfWork)
    {
        _campaignRepository = campaignRepository;
        _dateTimeProvider = dateTimeProvider;
        _cachingService = cachingService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<CampaignResult>> Handle(
        UpdateCampaignCommand request,
        CancellationToken cancellationToken)
    {
        var campaign = await _campaignRepository.GetById(
            CampaignId.Create(request.Id),
            cancellationToken);

        if (campaign is null)
            return Errors.Campaign.NotRegistered;

        var characterList = request.CharacterIds.ConvertAll(x => CharacterId.Create(x));

        campaign.Update(
            request.Name,
            request.Description,
            characterList,
            _dateTimeProvider.UtcNow
        );

        return await _unitOfWork.Execute(async _ =>
        {
            await _campaignRepository.Update(campaign);
            var result = campaign.Adapt<CampaignResult>();
            await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
            return result;
        },
        cancellationToken);
    }
}
