using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.AggregateSession.Events;

public record SessionFinishedDomainEvent(
) : IDomainEvent;
