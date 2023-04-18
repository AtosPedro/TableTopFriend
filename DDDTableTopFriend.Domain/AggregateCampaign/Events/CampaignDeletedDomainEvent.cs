using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateCampaign.Events;

public record CampaignDeletedDomainEvent(
    CampaignId CampaignId,
    DateTime DeletedAt
) : IDomainEvent;
