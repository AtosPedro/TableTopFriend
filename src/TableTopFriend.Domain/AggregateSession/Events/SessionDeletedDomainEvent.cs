using TableTopFriend.Domain.AggregateCampaign.ValueObjects;
using TableTopFriend.Domain.AggregateSession.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.AggregateSession.Events;

public record SessionDeletedDomainEvent(
    UserId UserId,
    SessionId SessionId,
    CampaignId CampaignId,
    DateTime DeletedAt
): IDomainEvent;
