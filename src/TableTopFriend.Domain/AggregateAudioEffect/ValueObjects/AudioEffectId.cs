using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Domain.AggregateAudioEffect.ValueObjects;

public sealed class AudioEffectId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private AudioEffectId(Guid value)
    {
        Value = value;
    }

    public static AudioEffectId CreateUnique() => new(Guid.NewGuid());
    public static AudioEffectId Create(Guid id) => new(id);
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private AudioEffectId()
    {
    }
#pragma warning restore CS8618
}
