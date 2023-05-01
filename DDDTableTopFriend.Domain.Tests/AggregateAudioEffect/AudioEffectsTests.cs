using DDDTableTopFriend.Domain.AggregateAudioEffect;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using NUnit.Framework;

namespace DDDTableTopFriend.Domain.Tests.AggregateAudioEffect;

[TestFixture]
public class AudioEffectsTests
{
    [Test]
    public void Create_Audio_Effect_Should_Return_Valid_Audio_Effect()
    {
        UserId userId = UserId.CreateUnique();
        const string name = "audio effect test";
        const string description = "audio effect test";
        const string? audioLink = "";
        const byte[]? audioClip = default;
        DateTime createdAt = DateTime.UtcNow;

        var audioEffect = AudioEffect.Create(
            userId,
            name,
            description,
            audioLink,
            audioClip,
            createdAt
        );

        Assert.Multiple(() =>
        {
            Assert.That(audioEffect.UserId, Is.EqualTo(userId));
            Assert.That(audioEffect.Name, Is.EqualTo(name));
            Assert.That(audioEffect.Description, Is.EqualTo(description));
            Assert.That(audioEffect.AudioLink, Is.EqualTo(audioLink));
            Assert.That(audioEffect.AudioClip, Is.EqualTo(audioClip));
            Assert.That(audioEffect.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void Update_Audio_Effect_Should_Update_Audio_Effect_Instance()
    {
        UserId userId = UserId.CreateUnique();
        const string name = "audio effect test";
        const string description = "audio effect test";
        const string? audioLink = "";
        const byte[]? audioClip = default;
        DateTime createdAt = DateTime.UtcNow;

        UserId userIdUpdated = UserId.CreateUnique();
        const string nameUpdated = "audio effect test Updated";
        const string descriptionUpdated = "audio effect test Updated";
        const string? audioLinkUpdated = "";
        const byte[]? audioClipUpdated = default;
        DateTime updatedAt = DateTime.UtcNow;

        var audioEffect = AudioEffect.Create(
            userId,
            name,
            description,
            audioLink,
            audioClip,
            createdAt
        );

        audioEffect.Update(
            nameUpdated,
            descriptionUpdated,
            audioLinkUpdated,
            audioClipUpdated,
            updatedAt
        );

        Assert.Multiple(() =>
        {
            Assert.That(audioEffect.UserId, Is.EqualTo(userId));
            Assert.That(audioEffect.Name, Is.EqualTo(nameUpdated));
            Assert.That(audioEffect.Description, Is.EqualTo(descriptionUpdated));
            Assert.That(audioEffect.AudioLink, Is.EqualTo(audioLinkUpdated));
            Assert.That(audioEffect.AudioClip, Is.EqualTo(audioClipUpdated));
            Assert.That(audioEffect.UpdatedAt, Is.EqualTo(updatedAt));
        });
    }
}
