using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateUser.Events;

public record DeletedUserDomainEvent(
    UserId userId,
    DateTime DeletedDate
): IDomainEvent;
