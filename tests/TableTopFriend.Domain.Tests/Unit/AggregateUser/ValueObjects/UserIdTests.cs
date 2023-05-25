using TableTopFriend.Domain.AggregateUser.ValueObjects;
using NUnit.Framework;

namespace TableTopFriend.Domain.Tests.AggregateUser.ValueObjects;

[TestFixture]
[Author("Atos Pedro")]
public class UserIdTests
{
    [Test]
    public void Create_Unique_Should_Return_UserId_Valid()
    {
        UserId userId = UserId.CreateUnique();
        Assert.Multiple(() =>
        {
            Assert.That(userId, Is.Not.Null);
            Assert.That(userId.Value, Is.Not.EqualTo(default(Guid)));
        });
    }

    [Test]
    public void Create_Should_Return_UserId_With_The_Passed_Value()
    {
        var id = Guid.NewGuid();
        UserId userId = UserId.Create(id);
        Assert.Multiple(() =>
        {
            Assert.That(userId, Is.Not.Null);
            Assert.That(userId.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(userId.Value, Is.EqualTo(id));
        });
    }
}
