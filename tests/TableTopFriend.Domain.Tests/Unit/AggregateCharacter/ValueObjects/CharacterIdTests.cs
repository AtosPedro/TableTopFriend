using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using NUnit.Framework;

namespace TableTopFriend.Domain.Tests.AggregateCharacter.ValueObjects;

[TestFixture]
public class CharacterIdTests
{
    [Test]
    public void Create_Unique_Should_Return_CharacterId_Valid()
    {
        CharacterId characterId = CharacterId.CreateUnique();
        Assert.Multiple(() =>
        {
            Assert.That(characterId, Is.Not.Null);
            Assert.That(characterId.Value, Is.Not.EqualTo(default(Guid)));
        });
    }

    [Test]
    public void Create_Should_Return_CharacterId_With_The_Passed_Value()
    {
        var id = Guid.NewGuid();
        CharacterId characterId = CharacterId.Create(id);
        Assert.Multiple(() =>
        {
            Assert.That(characterId, Is.Not.Null);
            Assert.That(characterId.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(characterId.Value, Is.EqualTo(id));
        });
    }
}
