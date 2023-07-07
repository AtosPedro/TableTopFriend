using Mapster;
using MediatR;
using TableTopFriend.Application.Campaigns.Common;
using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Domain.AggregateSession.Events;

namespace TableTopFriend.Application.Campaigns.EventHandlers;

public class AddSessionIdWhenSessionScheduled : INotificationHandler<SessionScheduledDomainEvent>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ICachingService _cachingService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public AddSessionIdWhenSessionScheduled(
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
        SessionScheduledDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var campaign = await _campaignRepository.GetById(
            notification.CampaignId,
            cancellationToken);

        if (campaign is null)
            return;

        campaign.AddSessionId(
            notification.SessionId,
            _dateTimeProvider.UtcNow
        );

        await _unitOfWork.Execute(async _ =>
        {
            await _campaignRepository.Update(campaign);
            var campaignResult = campaign.Adapt<CampaignResult>();
            await _cachingService.SetCacheValueAsync(notification.CampaignId.Value.ToString(), campaignResult);
            return campaignResult;
        },
        cancellationToken);
    }
}
