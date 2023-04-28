using DDDTableTopFriend.Application.Campaigns.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Delete;

public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand, ErrorOr<CampaignResult>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICachingService _cachingService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCampaignCommandHandler(
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
        DeleteCampaignCommand request,
        CancellationToken cancellationToken)
    {
        var campaign = await _campaignRepository.GetById(
            CampaignId.Create(request.Id),
            cancellationToken);

        if (campaign is null)
            return Errors.Campaign.NotRegistered;

        campaign.MarkToDelete(_dateTimeProvider.UtcNow);

        return await _unitOfWork.Execute(async _ =>
        {
            await _campaignRepository.Remove(campaign);
            await _cachingService.RemoveCacheValueAsync<CampaignResult>(campaign.Id.Value.ToString());
            await _cachingService.RemoveCacheValueAsync<CampaignJoinedResult>(campaign.Id.Value.ToString());
            return request.Adapt<CampaignResult>();
        },
        cancellationToken);
    }
}
