using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateStatus;
using DDDTableTopFriend.Domain.AggregateStatus.Events;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.ValueObjects;
using Moq;
using NUnit.Framework;

namespace DDDTableTopFriend.Domain.Tests.AggregateStatus;

[TestFixture]
public class StatusTests
{
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock = new();
    private readonly IDateTimeProvider _dateTimeProvider;

    public StatusTests()
    {
        var mockDate = DateTime.Parse("06/05/2023 00:00:00");
        _dateTimeProviderMock.Setup(x => x.UtcNow).Returns(
            mockDate
        );
        _dateTimeProvider = _dateTimeProviderMock.Object;
    }

    [Test]
    public void Create_Should_Return_Valid_Status()
    {
        var userId = UserId.CreateUnique();
        const string name = "status test";
        const string description = "status description test";
        const float quantity = 0;
        DateTime createdAt = _dateTimeProvider.UtcNow;

        Name nameVo = Name.Create(name).Value;
        Description descriptionVo = Description.Create(description).Value;

        var status = Status.Create(
            userId,
            name,
            description,
            quantity,
            createdAt
        ).Value;

        var domainEvent = status.GetDomainEvents().FirstOrDefault() as StatusCreatedDomainEvent;

        Assert.Multiple(() =>
        {
            Assert.That(status.Id.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(status.UserId, Is.EqualTo(userId));
            Assert.That(status.Name, Is.EqualTo(nameVo));
            Assert.That(status.Description, Is.EqualTo(descriptionVo));
            Assert.That(status.Quantity, Is.EqualTo(quantity));
            Assert.That(status.CreatedAt, Is.EqualTo(createdAt));
            Assert.That(domainEvent!.Name, Is.EqualTo(nameVo));
            Assert.That(domainEvent!.Description, Is.EqualTo(descriptionVo));
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
        DateTime createdAt = _dateTimeProvider.UtcNow;

        const string nameUpdated = "status test dest";
        const string descriptionUpdated = "status test desc updated";
        const float quantityUpdated = 12;
        DateTime updatedAt = _dateTimeProvider.UtcNow;

        Name nameUpdatedVo = Name.Create(nameUpdated).Value;
        Description descriptionUpdatedVo = Description.Create(descriptionUpdated).Value;

        var status = Status.Create(
            userId,
            name,
            description,
            quantity,
            createdAt
        ).Value;

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
            Assert.That(status.Name, Is.EqualTo(nameUpdatedVo));
            Assert.That(status.Description, Is.EqualTo(descriptionUpdatedVo));
            Assert.That(status.Quantity, Is.EqualTo(quantityUpdated));
            Assert.That(status.UpdatedAt, Is.EqualTo(updatedAt));
            Assert.That(domainEvent!.Name, Is.EqualTo(nameUpdatedVo));
            Assert.That(domainEvent!.Description, Is.EqualTo(descriptionUpdatedVo));
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
        DateTime createdAt = _dateTimeProvider.UtcNow;
        DateTime deletedAt = _dateTimeProvider.UtcNow;

        var status = Status.Create(
            userId,
            name,
            description,
            quantity,
            createdAt
        ).Value;

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
