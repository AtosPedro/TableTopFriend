using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using DDDTableTopFriend.Domain.Common.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateAudioEffect;

public sealed class AudioEffect : AggregateRoot<AudioEffectId, Guid>
{
    public UserId UserId { get; } = null!;
    public Name Name { get; } = null!;
    public Description Description { get; } = null!;
    public YoutubeVideoUrl? AudioLink { get; set; }
    public AudioClip? Clip { get; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    public AudioEffect(AudioEffectId id) : base(id) { }

    private AudioEffect(
        AudioEffectId id,
        UserId userId,
        Name name,
        Description description,
        YoutubeVideoUrl? audioLink,
        AudioClip? audioClip,
        DateTime createdAt) : base(id)
    {
        Name = name;
        Description = description;
        AudioLink = audioLink;
        Clip = audioClip;
        CreatedAt = createdAt;
        UserId = userId;
    }

    public static ErrorOr<AudioEffect> Create(
        UserId userId,
        string nameStr,
        string descriptionStr,
        string? audioLinkStr,
        byte[]? audioClipBuffer,
        DateTime createdAt)
    {
        if (userId is null)
            return Errors.User.InvalidId;

        var errors = new List<Error>();
        var name = Name.Create(nameStr);
        var description = Description.Create(descriptionStr);

        ErrorOr<YoutubeVideoUrl>? audioLink = null;
        ErrorOr<AudioClip>? clip = null;

        if (audioLinkStr is not null)
            audioLink = YoutubeVideoUrl.Create(audioLinkStr);

        if (audioClipBuffer is not null)
            clip = AudioClip.Create(audioClipBuffer);

        if (name.IsError)
            errors.AddRange(name.Errors);

        if (description.IsError)
            errors.AddRange(description.Errors);

        if (errors.Any())
            return errors;

        return new AudioEffect(
            AudioEffectId.CreateUnique(),
            userId,
            name.Value,
            description.Value,
            audioLink.Value.Value,
            clip.Value.Value,
            createdAt
        );
    }

    public ErrorOr<AudioEffect> Update(
        string nameStr,
        string descriptionStr,
        string? audioLink,
        byte[]? audioClip,
        DateTime updatedAt)
    {
        var name = Name.Create(nameStr);
        var description = Description.Create(descriptionStr);

        if (name.IsError)
            return name.Errors;

        if (description.IsError)
            return description.Errors;

        // AudioLink = audioLink;
        // AudioClip = audioClip;
        // UpdatedAt = updatedAt;

        return this;
    }

#pragma warning disable CS8618
    private AudioEffect()
    {
    }
#pragma warning restore CS8618
}
