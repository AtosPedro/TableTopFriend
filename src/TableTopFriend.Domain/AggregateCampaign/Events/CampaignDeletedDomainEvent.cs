using TableTopFriend.Domain.AggregateCampaign.ValueObjects;
using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.AggregateCampaign.Events;

public record CampaignDeletedDomainEvent(
    CampaignId CampaignId,
    DateTime DeletedAt
) : IDomainEvent;
