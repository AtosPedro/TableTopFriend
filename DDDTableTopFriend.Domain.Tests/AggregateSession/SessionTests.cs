using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSession;
using DDDTableTopFriend.Domain.AggregateSession.Events;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.ValueObjects;
using Moq;
using NUnit.Framework;

namespace DDDTableTopFriend.Domain.Tests.AggregateSession;

[TestFixture]
public class SessionTests
{
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock = new();
    private readonly IDateTimeProvider _dateTimeProvider;

    public SessionTests()
    {
        var mockDate = DateTime.Parse("06/05/2023 00:00:00");
        _dateTimeProviderMock.Setup(x => x.UtcNow).Returns(
            mockDate
        );
        _dateTimeProvider = _dateTimeProviderMock.Object;
    }

    [Test]
    public void Create_Session_Should_Return_Valid_Session()
    {
        UserId userId = UserId.CreateUnique();
        CampaignId campaignId = CampaignId.CreateUnique();
        const string name = "session test";
        const string description = "session description test";
        DateTime dateTime = _dateTimeProvider.UtcNow;
        DateTime createdAt = _dateTimeProvider.UtcNow;

        Name nameVo = Name.Create(name).Value;
        Description descriptionVo = Description.Create(description).Value;

        var session = Session.Create(
            userId,
            campaignId,
            name,
            description,
            dateTime,
            createdAt
        ).Value;

        var domainEvent = session.GetDomainEvents().FirstOrDefault() as SessionScheduledDomainEvent;

        Assert.Multiple(() =>
        {
            Assert.That(session.UserId, Is.EqualTo(userId));
            Assert.That(session.CampaignId, Is.EqualTo(campaignId));
            Assert.That(session.Name, Is.EqualTo(nameVo));
            Assert.That(session.Description, Is.EqualTo(descriptionVo));
            Assert.That(session.DateTime, Is.EqualTo(dateTime));
            Assert.That(session.CreatedAt, Is.EqualTo(createdAt));
            Assert.That(domainEvent!.CampaignId, Is.EqualTo(campaignId));
            Assert.That(domainEvent!.SessionId, Is.EqualTo(SessionId.Create(session.Id.Value)));
            Assert.That(domainEvent!.UserId, Is.EqualTo(userId));
        });
    }

    [Test]
    public void Update_Session_Should_Update_The_Session_Instance()
    {
        UserId userId = UserId.CreateUnique();
        CampaignId campaignId = CampaignId.CreateUnique();
        const string name = "session test";
        const string description = "session description test";
        DateTime dateTime = _dateTimeProvider.UtcNow;
        DateTime createdAt = _dateTimeProvider.UtcNow;

        const string nameUpdated = "session test Updated";
        const string descriptionUpdated = "session description test Updated";
        DateTime dateTimeUpdated = _dateTimeProvider.UtcNow;
        TimeSpan durationUpdated = TimeSpan.FromHours(2);
        DateTime updatedAt = _dateTimeProvider.UtcNow;

        Name nameUpdatedVo = Name.Create(nameUpdated).Value;
        Description descriptionUpdatedVo = Description.Create(descriptionUpdated).Value;

        var session = Session.Create(
            userId,
            campaignId,
            name,
            description,
            dateTime,
            createdAt
        ).Value;

        session.ClearDomainEvents();
        session.Update(
            nameUpdated,
            descriptionUpdated,
            dateTimeUpdated,
            durationUpdated,
            updatedAt
        );

        var domainEvent = session.GetDomainEvents().FirstOrDefault() as SessionChangedDomainEvent;
        Assert.Multiple(() =>
        {
            Assert.That(session.UserId, Is.EqualTo(userId));
            Assert.That(session.CampaignId, Is.EqualTo(campaignId));
            Assert.That(session.Name, Is.EqualTo(nameUpdatedVo));
            Assert.That(session.Description, Is.EqualTo(descriptionUpdatedVo));
            Assert.That(session.DateTime, Is.EqualTo(dateTime));
            Assert.That(session.UpdatedAt, Is.EqualTo(updatedAt));
            Assert.That(domainEvent!.CampaignId, Is.EqualTo(campaignId));
            Assert.That(domainEvent!.SessionId, Is.EqualTo(SessionId.Create(session.Id.Value)));
            Assert.That(domainEvent!.UserId, Is.EqualTo(userId));
        });
    }

    [Test]
    public void Mark_To_Delete_Should_Return_Deleted_Session_Domain_Event_Valid()
    {
        UserId userId = UserId.CreateUnique();
        CampaignId campaignId = CampaignId.CreateUnique();
        const string name = "session test";
        const string description = "session description test";
        DateTime dateTime = _dateTimeProvider.UtcNow;
        DateTime createdAt = _dateTimeProvider.UtcNow;
        DateTime deletedAt = _dateTimeProvider.UtcNow;

        var session = Session.Create(
            userId,
            campaignId,
            name,
            description,
            dateTime,
            createdAt
        ).Value;

        session.ClearDomainEvents();
        session.MarkToDelete(deletedAt);
        var domainEvent = session.GetDomainEvents().FirstOrDefault() as SessionDeletedDomainEvent;
        Assert.Multiple(() =>
        {
            Assert.That(domainEvent!.CampaignId, Is.EqualTo(campaignId));
            Assert.That(domainEvent!.SessionId, Is.EqualTo(SessionId.Create(session.Id.Value)));
            Assert.That(domainEvent!.UserId, Is.EqualTo(userId));
            Assert.That(domainEvent!.DeletedAt, Is.EqualTo(deletedAt));
        });
    }
}
