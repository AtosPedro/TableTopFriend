using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Common.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateStatus.Events;

public record StatusChangedDomainEvent(
    StatusId StatusId,
    Name Name,
    Description Description,
    float Quantity,
    DateTime UpdatedAt
) : IDomainEvent;
