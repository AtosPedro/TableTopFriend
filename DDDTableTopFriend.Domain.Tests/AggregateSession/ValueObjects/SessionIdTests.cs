using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using NUnit.Framework;

namespace DDDTableTopFriend.Domain.Tests.AggregateSession.ValueObjects;

[TestFixture]
public class SessionIdTests
{
    [Test]
    public void Create_Unique_Should_Return_SessionId_Valid()
    {
        SessionId sessionId = SessionId.CreateUnique();
        Assert.Multiple(() =>
        {
            Assert.That(sessionId, Is.Not.Null);
            Assert.That(sessionId.Value, Is.Not.EqualTo(default(Guid)));
        });
    }

    [Test]
    public void Create_Should_Return_SessionId_With_The_Passed_Value()
    {
        var id = Guid.NewGuid();
        SessionId sessionId = SessionId.Create(id);
        Assert.Multiple(() =>
        {
            Assert.That(sessionId, Is.Not.Null);
            Assert.That(sessionId.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(sessionId.Value, Is.EqualTo(id));
        });
    }
}
