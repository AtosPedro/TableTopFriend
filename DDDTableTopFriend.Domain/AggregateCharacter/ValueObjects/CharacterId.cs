using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Common.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;

public sealed class CharacterId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private CharacterId(Guid value)
    {
        Value = value;
    }

    public static CharacterId CreateUnique() => new (Guid.NewGuid());
    public static CharacterId Create(Guid id) => new (id);
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
