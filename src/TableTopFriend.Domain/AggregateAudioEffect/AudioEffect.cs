using TableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.Common.Errors;
using ErrorOr;
using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Domain.AggregateAudioEffect;

public sealed class AudioEffect : AggregateRoot<AudioEffectId, Guid>
{
    public UserId UserId { get; private set; } = null!;
    public Name Name { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
    public YoutubeVideoUrl? AudioLink { get; set; }
    public AudioClip? Clip { get; private set; }
    public DateTime CreatedAt { get; private set; }
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
            audioLink?.Value,
            clip?.Value,
            createdAt
        );
    }

    public ErrorOr<AudioEffect> Update(
        string nameStr,
        string descriptionStr,
        string? audioLinkStr,
        byte[]? audioClipBuffer,
        DateTime updatedAt)
    {
        ErrorOr<YoutubeVideoUrl>? audioLink = null;
        ErrorOr<AudioClip>? clip = null;
        var errors = new List<Error>();
        var name = Name.Create(nameStr);
        var description = Description.Create(descriptionStr);

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

        Name = name.Value ?? Name;
        Description = description.Value ?? Description;
        AudioLink = audioLink?.Value ?? AudioLink;
        Clip = clip?.Value ?? Clip;
        UpdatedAt = updatedAt;

        return this;
    }

#pragma warning disable CS8618
    private AudioEffect()
    {
    }
#pragma warning restore CS8618
}
