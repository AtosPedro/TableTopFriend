using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Enums;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateUser.Events;

public record UserChangedDomainEvent(
    UserId Id,
    string FirstName,
    string LastName,
    string Email,
    UserRole Role,
    DateTime UpdatedAt
) : IDomainEvent;
