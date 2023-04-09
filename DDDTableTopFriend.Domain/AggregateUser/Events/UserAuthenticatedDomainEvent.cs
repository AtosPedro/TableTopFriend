using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateUser.Events;

public record UserAuthenticatedDomainEvent(
    UserId Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime AuthenticatedAt
) : IDomainEvent;
