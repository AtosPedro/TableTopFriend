using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateStatus.Events;

public record StatusDeletedDomainEvent(
    StatusId StatusId,
    UserId UserId,
    DateTime DeletedAt
) : IDomainEvent;
