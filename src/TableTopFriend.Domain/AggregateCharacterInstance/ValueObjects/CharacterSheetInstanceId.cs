using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.AggregateCharacterInstance.ValueObjects;

public class CharacterSheetInstanceId : ValueObject
{
    public  Guid Value { get; protected set; }

    private CharacterSheetInstanceId(Guid value)
    {
        Value = value;
    }

    public static CharacterSheetInstanceId CreateUnique() => new(Guid.NewGuid());

    public static CharacterSheetInstanceId Create(Guid id) => new(id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}