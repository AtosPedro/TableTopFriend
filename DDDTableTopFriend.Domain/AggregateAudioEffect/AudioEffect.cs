using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateAudioEffect;

public sealed class AudioEffect : AggregateRoot<AudioEffectId>
{
    public AudioEffect(AudioEffectId id) : base(id)
    {
    }
}
