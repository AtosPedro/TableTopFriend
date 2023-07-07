using TableTopFriend.Domain.AggregateStatus.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.AggregateStatus.Events;

public record StatusDeletedDomainEvent(
    StatusId StatusId,
    UserId UserId,
    DateTime DeletedAt
) : IDomainEvent;
