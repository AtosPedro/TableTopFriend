using MediatR;
using TableTopFriend.Application.Campaigns.Common;
using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Domain.AggregateSession.Events;

namespace TableTopFriend.Application.Campaigns.EventHandlers;

public class RemoveSessionIdWhenSessionDeleted : INotificationHandler<SessionDeletedDomainEvent>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ICachingService _cachingService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveSessionIdWhenSessionDeleted(
        ICampaignRepository campaignRepository,
        ICachingService cachingService,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork)
    {
        _campaignRepository = campaignRepository;
        _cachingService = cachingService;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        SessionDeletedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var campaign = await _campaignRepository.GetById(
            notification.CampaignId,
            cancellationToken
        );

        if (campaign is null)
            return;

        campaign.RemoveSessionId(
            notification.SessionId,
            _dateTimeProvider.UtcNow
        );

        await _unitOfWork.Execute(async _ =>
        {
            await _campaignRepository.Update(campaign);
            await _cachingService.RemoveCacheValueAsync<CampaignResult>(notification.CampaignId.Value.ToString());
            return true;
        },
        cancellationToken);
    }
}
