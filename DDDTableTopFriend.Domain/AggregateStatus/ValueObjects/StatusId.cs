using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;

public sealed class StatusId : ValueObject
{
    public Guid Value { get; }

    private StatusId(Guid value)
    {
        Value = value;
    }

    public static StatusId CreateUnique() => new (Guid.NewGuid());
    public static StatusId Create(Guid id) => new (id);
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
