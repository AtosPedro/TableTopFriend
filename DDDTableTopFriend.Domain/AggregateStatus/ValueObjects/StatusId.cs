using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Common.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;

public sealed class StatusId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

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
