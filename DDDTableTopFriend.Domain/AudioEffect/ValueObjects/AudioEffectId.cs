using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AudioEffect.ValueObjects;

public sealed class AudioEffectId : ValueObject
{
    public Guid Value { get; }

    private AudioEffectId(Guid value)
    {
        Value = value;
    }

    public static AudioEffectId CreateUnique() => new (Guid.NewGuid());
    public static AudioEffectId Create(Guid id) => new (id);
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
