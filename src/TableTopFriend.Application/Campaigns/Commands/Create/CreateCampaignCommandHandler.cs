using TableTopFriend.Application.Campaigns.Common;
using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Domain.AggregateCampaign;
using TableTopFriend.Domain.Common.Errors;
using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using ErrorOr;
using Mapster;
using MediatR;
using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Application.Campaigns.Commands.Create;

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
        var name = Name.Create(request.Name).Value;

        var campaign = await _campaignRepository.GetByName(
            name,
            UserId.Create(request.UserId),
            cancellationToken);

        if (campaign is not null)
            return Errors.Campaign.DuplicateName;

        var campaignOrError = Campaign.Create(
            UserId.Create(request.UserId),
            request.Name,
            request.Description,
            request.CharacterIds.ConvertAll(x => CharacterId.Create(x)),
            _dateTimeProvider.UtcNow
        );

        if (campaignOrError.IsError)
            return campaignOrError.Errors;

        return await _unitOfWork.Execute(async cancellationToken =>
        {
            await _campaignRepository.Add(
                campaignOrError.Value,
                cancellationToken);
            var result = campaignOrError.Value.Adapt<CampaignResult>();
            await _cachingService.SetCacheValueAsync(
                result.Id.ToString(),
                result);
            return result;
        },
        cancellationToken);
    }
}
