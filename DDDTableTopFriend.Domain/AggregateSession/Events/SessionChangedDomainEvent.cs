using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateSession.Events;

public record SessionChangedDomainEvent(
    UserId UserId,
    CampaignId CampaignId,
    SessionId SessionId
) : IDomainEvent;
