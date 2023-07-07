using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.AggregateUser.Enums;
using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.AggregateUser.Events;

public record UserChangedDomainEvent(
    UserId Id,
    string FirstName,
    string LastName,
    Email Email,
    UserRole Role,
    DateTime UpdatedAt
) : IDomainEvent;
