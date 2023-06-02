using Mapster;
using MediatR;
using TableTopFriend.Application.Campaigns.Common;
using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Domain.AggregateCharacter.Events;

namespace TableTopFriend.Application.Campaigns.EventHandlers;

public class RemoveCharacterIdWhenCharacterDeleted : INotificationHandler<CharacterDeletedDomainEvent>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ICachingService _cachingService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveCharacterIdWhenCharacterDeleted(
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
        CharacterDeletedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var campaigns = await _campaignRepository.GetAll(
            notification.UserId,
            cancellationToken
        );

        var campaignsWithTheCharacter = campaigns.Where(c =>
            c.HasCharacter(notification.CharacterId)
        ).ToList();

        foreach (var campaign in campaignsWithTheCharacter)
        {
            campaign.RemoveCharacterId(
                notification.CharacterId,
                _dateTimeProvider.UtcNow);
        }

        await _unitOfWork.Execute(async _ =>
        {
            foreach (var campaign in campaignsWithTheCharacter)
            {
                await _campaignRepository.Update(campaign);
                var campaignResult = campaign.Adapt<CampaignResult>();
                await _cachingService.SetCacheValueAsync(campaignResult.Id.ToString(), campaignResult);
            }

            return true;
        },
        cancellationToken);
    }
}
