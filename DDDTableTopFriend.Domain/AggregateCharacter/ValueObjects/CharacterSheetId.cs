using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;

public sealed class CharacterSheetId : ValueObject
{
    public Guid Value { get; }

    private CharacterSheetId(Guid value)
    {
        Value = value;
    }

    public static CharacterSheetId CreateUnique() => new (Guid.NewGuid());
    public static CharacterSheetId Create(Guid id) => new (id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}