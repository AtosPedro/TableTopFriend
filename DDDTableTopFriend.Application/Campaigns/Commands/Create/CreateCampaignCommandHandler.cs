using DDDTableTopFriend.Application.Campaigns.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateCampaign;
using DDDTableTopFriend.Domain.Common.Errors;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Create;

public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, ErrorOr<CampaignResult>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICachingService _cachingService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCampaignCommandHandler(
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
        CreateCampaignCommand request,
        CancellationToken cancellationToken)
    {
        var campaign = await _campaignRepository.GetByName(
            request.Name,
            UserId.Create(request.UserId),
            cancellationToken);

        if (campaign is not null)
            return Errors.Campaign.DuplicateName;

        campaign = Campaign.Create(
            UserId.Create(request.UserId),
            request.Name,
            request.Description,
            request.CharacterIds.ConvertAll(x => CharacterId.Create(x)),
            _dateTimeProvider.UtcNow
        );

        return await _unitOfWork.Execute(async cancellationToken =>
        {
            await _campaignRepository.Add(
                campaign,
                cancellationToken);
            var result = campaign.Adapt<CampaignResult>();
            await _cachingService.SetCacheValueAsync(
                result.Id.ToString(),
                result);
            return result;
        },
        cancellationToken);
    }
}
