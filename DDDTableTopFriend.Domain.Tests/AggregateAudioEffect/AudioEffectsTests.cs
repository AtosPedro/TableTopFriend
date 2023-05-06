using System.Text;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateAudioEffect;
using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.ValueObjects;
using Moq;
using NUnit.Framework;

namespace DDDTableTopFriend.Domain.Tests.AggregateAudioEffect;

[TestFixture]
public class AudioEffectsTests
{
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock = new();
    private readonly IDateTimeProvider _dateTimeProvider;

    public AudioEffectsTests()
    {
        var mockDate = DateTime.Parse("06/05/2023 00:00:00");
        _dateTimeProviderMock.Setup(x => x.UtcNow).Returns(
            mockDate
        );
        _dateTimeProvider = _dateTimeProviderMock.Object;
    }

    [Test]
    public void Create_Audio_Effect_Should_Return_Valid_Audio_Effect()
    {
        UserId userId = UserId.CreateUnique();
        const string name = "audio effect test";
        const string description = "audio effect test";
        const string? audioLink = "https://www.youtube.com/watch?v=2m2520TuUdk&t=14s";
        byte[]? audioClip = Encoding.UTF8.GetBytes("only for get the bytes for test"); ;
        DateTime createdAt = _dateTimeProvider.UtcNow;

        var nameUpdatedVo = Name.Create(name).Value;
        var descriptionUpdatedVo = Description.Create(description).Value;
        var audioLinkUpdatedVo = YoutubeVideoUrl.Create(audioLink).Value;
        var audioClipUpdatedVo = AudioClip.Create(audioClip).Value;

        var audioEffect = AudioEffect.Create(
            userId,
            name,
            description,
            audioLink,
            audioClip,
            createdAt
        ).Value;

        Assert.Multiple(() =>
        {
            Assert.That(audioEffect.UserId, Is.EqualTo(userId));
            Assert.That(audioEffect.Name.Equals(nameUpdatedVo));
            Assert.That(audioEffect.Description.Equals(descriptionUpdatedVo));
            Assert.That(audioEffect.AudioLink!.Equals(audioLinkUpdatedVo));
            Assert.That(audioEffect.Clip!.Equals(audioClipUpdatedVo));
            Assert.That(audioEffect.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void Update_Audio_Effect_Should_Update_Audio_Effect_Instance()
    {
        UserId userId = UserId.CreateUnique();
        const string name = "audio effect test";
        const string description = "audio effect test";
        const string? audioLink = "https://www.youtube.com/watch?v=2m2520TuUdk&t=14s";
        byte[]? audioClip = Encoding.UTF8.GetBytes("only for get the bytes for test"); ;
        DateTime createdAt = _dateTimeProvider.UtcNow;

        UserId userIdUpdated = UserId.CreateUnique();
        const string nameUpdated = "audio effect test Updated";
        const string descriptionUpdated = "audio effect test Updated";
        const string? audioLinkUpdated = "https://www.youtube.com/watch?v=kzgFwZEAHZQ";
        byte[]? audioClipUpdated = Encoding.UTF8.GetBytes("only for get the bytes for test update");
        DateTime updatedAt = _dateTimeProvider.UtcNow;

        Name nameUpdatedVo = Name.Create(nameUpdated).Value;
        Description descriptionUpdatedVo = Description.Create(descriptionUpdated).Value;
        YoutubeVideoUrl audioLinkUpdatedVo = YoutubeVideoUrl.Create(audioLinkUpdated).Value;
        AudioClip audioClipUpdatedVo = AudioClip.Create(audioClipUpdated).Value;

        var audioEffect = AudioEffect.Create(
            userId,
            name,
            description,
            audioLink,
            audioClip,
            createdAt
        ).Value;

        audioEffect = audioEffect.Update(
            nameUpdated,
            descriptionUpdated,
            audioLinkUpdated,
            audioClipUpdated,
            updatedAt
        ).Value;

        Assert.Multiple(() =>
        {
            Assert.That(audioEffect.UserId, Is.EqualTo(userId));
            Assert.That(audioEffect.Name, Is.EqualTo(nameUpdatedVo));
            Assert.That(audioEffect.Description, Is.EqualTo(descriptionUpdatedVo));
            Assert.That(audioEffect.AudioLink, Is.EqualTo(audioLinkUpdatedVo));
            Assert.That(audioEffect.Clip, Is.EqualTo(audioClipUpdatedVo));
            Assert.That(audioEffect.UpdatedAt, Is.EqualTo(updatedAt));
        });
    }
}
