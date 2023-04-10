using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateAudioEffect;

public sealed class AudioEffect : AggregateRoot<AudioEffectId>
{
    public string Name { get; }
    public string Description { get; }
    public string AudioLink { get; }
    public byte[] AudioClip { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }

    public AudioEffect(AudioEffectId id) : base(id) { }

    public AudioEffect(
        AudioEffectId id,
        string name,
        string description,
        string audioLink,
        byte[] audioClip,
        DateTime createdAt,
        DateTime updatedAt) : base(id)
    {
        Name = name;
        Description = description;
        AudioLink = audioLink;
        AudioClip = audioClip;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
