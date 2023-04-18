using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateStatus.Events;

public record StatusChangedDomainEvent(
    StatusId StatusId,
    string Name,
    string Description,
    float Quantity,
    DateTime UpdatedAt
) : IDomainEvent;
