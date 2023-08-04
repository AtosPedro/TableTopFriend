using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Domain.AggregateCharacter.ValueObjects;

public sealed class CharacterId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private CharacterId(Guid value)
    {
        Value = value;
    }

    public static CharacterId CreateUnique() => new(Guid.NewGuid());
    public static CharacterId Create(Guid id) => new(id);
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
#pragma warning disable CS8618
    private CharacterId()
    {
    }
#pragma warning restore CS8618
}
