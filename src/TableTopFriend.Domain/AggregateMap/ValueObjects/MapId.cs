using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Domain.AggregateMap.ValueObjects;

public sealed class MapId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private MapId(Guid value)
    {
        Value = value;
    }

    public static MapId CreateUnique() => new (Guid.NewGuid());
    public static MapId Create(Guid value) => new (value);
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
