using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.Common.ValueObjects;

public abstract class AggregateRootId<TId> : ValueObject
{
    public abstract TId Value { get; protected set; }
}
