using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateStatus.Events;

public record StatusCreatedDomainEvent(
    StatusId StatusId,
    string Name,
    string Description,
    float Quantity,
    DateTime CreatedAt
) : IDomainEvent;
