using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Domain.AggregateStatus.ValueObjects;

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
    
#pragma warning disable CS8618
    private StatusId()
    {
    }
#pragma warning restore CS8618
}
