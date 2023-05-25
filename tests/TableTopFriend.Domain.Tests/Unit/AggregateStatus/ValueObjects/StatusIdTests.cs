using TableTopFriend.Domain.AggregateStatus.ValueObjects;
using NUnit.Framework;

namespace TableTopFriend.Domain.Tests.AggregateStatus.ValueObjects;

[TestFixture]
public class StatusIdTests
{
    [Test]
    public void Create_Unique_Should_Return_StatusId_Valid()
    {
        StatusId statusId = StatusId.CreateUnique();
        Assert.Multiple(() =>
        {
            Assert.That(statusId, Is.Not.Null);
            Assert.That(statusId.Value, Is.Not.EqualTo(default(Guid)));
        });
    }

    [Test]
    public void Create_Should_Return_StatusId_With_The_Passed_Value()
    {
        var id = Guid.NewGuid();
        StatusId statusId = StatusId.Create(id);
        Assert.Multiple(() =>
        {
            Assert.That(statusId, Is.Not.Null);
            Assert.That(statusId.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(statusId.Value, Is.EqualTo(id));
        });
    }
}

