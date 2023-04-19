using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateAudioEffect;

public sealed class AudioEffect : AggregateRoot<AudioEffectId, Guid>
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string? AudioLink { get; private set; } = null!;
    public byte[]? AudioClip { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public AudioEffect(AudioEffectId id) : base(id) { }

    private AudioEffect(
        AudioEffectId id,
        string name,
        string description,
        string? audioLink,
        byte[]? audioClip,
        DateTime createdAt) : base(id)
    {
        Name = name;
        Description = description;
        AudioLink = audioLink;
        AudioClip = audioClip;
        CreatedAt = createdAt;
    }

    public static AudioEffect Create(
        string name,
        string description,
        string? audioLink,
        byte[]? audioClip,
        DateTime createdAt)
    {
        AudioEffect audioEffect =  new(
            AudioEffectId.CreateUnique(),
            name,
            description,
            audioLink,
            audioClip,
            createdAt
        );
        return audioEffect;
    }

    public void Update(
        string name,
        string description,
        string? audioLink,
        byte[]? audioClip,
        DateTime updatedAt)
    {
        Name = name;
        Description = description;
        AudioLink = audioLink;
        AudioClip = audioClip;
        UpdatedAt = updatedAt;
    }

#pragma warning disable CS8618
    private AudioEffect()
    {
    }
#pragma warning restore CS8618
}
