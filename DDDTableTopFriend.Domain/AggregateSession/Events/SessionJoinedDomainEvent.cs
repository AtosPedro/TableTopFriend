using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateSession.Events;

public record SessionJoinedDomainEvent(
) : IDomainEvent;
