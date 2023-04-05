using DDDTableTopFriend.Domain.AudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AudioEffect;

public sealed class AudioEffect : AggregateRoot<AudioEffectId>
{
    public AudioEffect(AudioEffectId id) : base(id)
    {
    }
}
