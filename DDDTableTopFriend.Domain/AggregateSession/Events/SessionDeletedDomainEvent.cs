using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateSession.Events;

public record SessionDeletedDomainEvent(
    UserId UserId,
    SessionId SessionId,
    CampaignId CampaignId,
    DateTime DeletedAt
): IDomainEvent;
