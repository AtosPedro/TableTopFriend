using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.AggregateUser.Events;

public record DeletedUserDomainEvent(
    UserId userId,
    DateTime DeletedDate
): IDomainEvent;
