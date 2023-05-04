using DDDTableTopFriend.Domain.AggregateStatus;
using DDDTableTopFriend.Domain.AggregateStatus.Events;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using NUnit.Framework;

namespace DDDTableTopFriend.Domain.Tests.AggregateStatus;

[TestFixture]
public class StatusTests
{
    [Test]
    public void Create_Should_Return_Valid_Status()
    {
        var userId = UserId.CreateUnique();
        const string name = "";
        const string description = "";
        const float quantity = 0;
        DateTime createdAt = DateTime.UtcNow;

        var status = Status.Create(
            userId,
            name,
            description,
            quantity,
            createdAt
        );

        var domainEvent = status.GetDomainEvents().FirstOrDefault() as StatusCreatedDomainEvent;

        Assert.Multiple(() =>
        {
            Assert.That(status.Id.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(status.UserId, Is.EqualTo(userId));
            Assert.That(status.Name, Is.EqualTo(name));
            Assert.That(status.Description, Is.EqualTo(description));
            Assert.That(status.Quantity, Is.EqualTo(quantity));
            Assert.That(status.CreatedAt, Is.EqualTo(createdAt));
            Assert.That(domainEvent!.Name, Is.EqualTo(name));
            Assert.That(domainEvent!.Description, Is.EqualTo(description));
            Assert.That(domainEvent!.Quantity, Is.EqualTo(quantity));
            Assert.That(domainEvent!.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void Update_Status_Should_Return_Valid_Status()
    {
        var userId = UserId.CreateUnique();
        const string name = "status test";
        const string description = "status test desc";
        const float quantity = 10;
        DateTime createdAt = DateTime.UtcNow;

        const string nameUpdated = "status test dest";
        const string descriptionUpdated = "status test desc updated";
        const float quantityUpdated = 12;
        DateTime updatedAt = DateTime.UtcNow;

        var status = Status.Create(
            userId,
            name,
            description,
            quantity,
            createdAt
        );

        status.ClearDomainEvents();
        status.Update(
            nameUpdated,
            descriptionUpdated,
            quantityUpdated,
            updatedAt
        );

        var domainEvent = status.GetDomainEvents().FirstOrDefault() as StatusChangedDomainEvent;

        Assert.Multiple(() =>
        {
            Assert.That(status.Name, Is.EqualTo(nameUpdated));
            Assert.That(status.Description, Is.EqualTo(descriptionUpdated));
            Assert.That(status.Quantity, Is.EqualTo(quantityUpdated));
            Assert.That(status.UpdatedAt, Is.EqualTo(updatedAt));
            Assert.That(domainEvent!.Name, Is.EqualTo(nameUpdated));
            Assert.That(domainEvent!.Description, Is.EqualTo(descriptionUpdated));
            Assert.That(domainEvent!.Quantity, Is.EqualTo(quantityUpdated));
            Assert.That(domainEvent!.UpdatedAt, Is.EqualTo(updatedAt));
        });
    }

    [Test]
    public void Mark_To_Delete_Should_Return_Deleted_Status_Domain_Event_Valid()
    {
        var userId = UserId.CreateUnique();
        const string name = "status test";
        const string description = "status test desc";
        const float quantity = 10;
        DateTime createdAt = DateTime.UtcNow;
        DateTime deletedAt = DateTime.UtcNow;

        var status = Status.Create(
            userId,
            name,
            description,
            quantity,
            createdAt
        );

        status.ClearDomainEvents();
        status.MarkToDelete(deletedAt);
        var domainEvent = status.GetDomainEvents().FirstOrDefault() as StatusDeletedDomainEvent;

        Assert.Multiple(() =>
        {
            Assert.That(domainEvent, Is.Not.Null);
            Assert.That(domainEvent!.DeletedAt, Is.EqualTo(deletedAt));
            Assert.That(domainEvent!.StatusId, Is.EqualTo(StatusId.Create(status.Id.Value)));
            Assert.That(domainEvent!.UserId, Is.EqualTo(userId));
        });
    }
}