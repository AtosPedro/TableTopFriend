using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.AggregateCharacter.ValueObjects;

public sealed class CharacterSheetId : ValueObject
{
    public  Guid Value { get; set; }

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
