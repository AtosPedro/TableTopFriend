using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;

public sealed class CharacterId : ValueObject
{
    public Guid Value { get; }

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
