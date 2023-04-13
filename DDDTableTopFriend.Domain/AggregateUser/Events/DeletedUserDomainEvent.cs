using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateUser.Events;

public record DeletedUserDomainEvent(
    UserId userId,
    string FirstName,
    string LastName,
    string Email,
    DateTime DeletedDate
): IDomainEvent;
