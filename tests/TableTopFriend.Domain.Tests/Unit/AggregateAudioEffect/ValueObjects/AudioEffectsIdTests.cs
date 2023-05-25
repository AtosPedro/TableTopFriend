using TableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using NUnit.Framework;

namespace TableTopFriend.Domain.Tests.AggregateAudioEffect.ValueObjects;

[TestFixture]
public class AudioEffectsIdTests
{
    [Test]
    public void Create_Unique_Should_Return_AudioEffectId_Valid()
    {
        AudioEffectId audioEffectId = AudioEffectId.CreateUnique();
        Assert.Multiple(() =>
        {
            Assert.That(audioEffectId, Is.Not.Null);
            Assert.That(audioEffectId.Value, Is.Not.EqualTo(default(Guid)));
        });
    }

    [Test]
    public void Create_Should_Return_UserId_With_The_Passed_Value()
    {
        var id = Guid.NewGuid();
        AudioEffectId audioEffectId = AudioEffectId.Create(id);
        Assert.Multiple(() =>
        {
            Assert.That(audioEffectId, Is.Not.Null);
            Assert.That(audioEffectId.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(audioEffectId.Value, Is.EqualTo(id));
        });
    }
}
