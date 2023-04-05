using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.Character.ValueObjects;

public sealed class CharacterSheetId : ValueObject
{
    public Guid Value { get; }

    private CharacterSheetId(Guid value)
    {
        Value = value;
    }

    public static CharacterSheetId CreateUnique() => new (Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
