using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Domain.AggregateCharacterInstance.ValueObjects;

public class CharacterInstanceId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private CharacterInstanceId(Guid value)
    {
        Value = value;
    }

    public static CharacterInstanceId CreateUnique() => new(Guid.NewGuid());

    public static CharacterInstanceId Create(Guid id) => new(id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}