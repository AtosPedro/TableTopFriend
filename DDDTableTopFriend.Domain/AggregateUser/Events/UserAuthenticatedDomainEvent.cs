using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Enums;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateUser.Events;

public record UserAuthenticatedDomainEvent(
    UserId Id,
    string FirstName,
    string LastName,
    Email Email,
    UserRole Role,
    DateTime AuthenticatedAt
) : IDomainEvent;
