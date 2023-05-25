using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Enums;
using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.AggregateUser.Events;

public record UserRegisteredDomainEvent(
    UserId Id,
    string FirstName,
    string LastName,
    Email Email,
    UserRole Role,
    DateTime? CreatedAt
) : IDomainEvent;
