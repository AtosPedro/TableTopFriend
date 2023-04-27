using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Common.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;

public sealed class AudioEffectId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    public AudioEffectId(Guid value)
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
