using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.Character.ValueObjects;

public sealed class CharacterId : ValueObject
{
    public Guid Value { get; }

    private CharacterId(Guid value)
    {
        Value = value;
    }

    public static CharacterId CreateUnique() => new (Guid.NewGuid());
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
