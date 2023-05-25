using TableTopFriend.Domain.AggregateStatus.ValueObjects;
using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Domain.AggregateStatus.Events;

public record StatusCreatedDomainEvent(
    StatusId StatusId,
    Name Name,
    Description Description,
    float Quantity,
    DateTime CreatedAt
) : IDomainEvent;
