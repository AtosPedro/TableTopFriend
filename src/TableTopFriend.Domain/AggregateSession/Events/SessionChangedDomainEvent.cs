using TableTopFriend.Domain.AggregateCampaign.ValueObjects;
using TableTopFriend.Domain.AggregateSession.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.AggregateSession.Events;

public record SessionChangedDomainEvent(
    UserId UserId,
    CampaignId CampaignId,
    SessionId SessionId
) : IDomainEvent;
