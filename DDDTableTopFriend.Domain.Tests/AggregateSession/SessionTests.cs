using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSession;
using DDDTableTopFriend.Domain.AggregateSession.Events;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using NUnit.Framework;

namespace DDDTableTopFriend.Domain.Tests.AggregateSession;

[TestFixture]
public class SessionTests
{
    [Test]
    public void Create_Session_Should_Return_Valid_Session()
    {
        UserId userId = UserId.CreateUnique();
        CampaignId campaignId = CampaignId.CreateUnique();
        const string name = "session test";
        DateTime dateTime = DateTime.UtcNow;
        DateTime createdAt = DateTime.UtcNow;

        var session = Session.Create(
            userId,
            campaignId,
            name,
            dateTime,
            createdAt
        );

        var domainEvent = session.GetDomainEvents().FirstOrDefault() as SessionScheduledDomainEvent;

        Assert.Multiple(() =>
        {
            Assert.That(session.UserId, Is.EqualTo(userId));
            Assert.That(session.CampaignId, Is.EqualTo(campaignId));
            Assert.That(session.Name, Is.EqualTo(name));
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
        DateTime dateTime = DateTime.UtcNow;
        DateTime createdAt = DateTime.UtcNow;

        const string nameUpdated = "session test";
        DateTime dateTimeUpdated = DateTime.UtcNow;
        TimeSpan durationUpdated = TimeSpan.FromHours(2);
        DateTime updatedAt = DateTime.UtcNow;

        var session = Session.Create(
            userId,
            campaignId,
            name,
            dateTime,
            createdAt
        );

        session.ClearDomainEvents();
        session.Update(
            nameUpdated,
            dateTimeUpdated,
            durationUpdated,
            updatedAt
        );

        var domainEvent = session.GetDomainEvents().FirstOrDefault() as SessionChangedDomainEvent;
        Assert.Multiple(() =>
        {
            Assert.That(session.UserId, Is.EqualTo(userId));
            Assert.That(session.CampaignId, Is.EqualTo(campaignId));
            Assert.That(session.Name, Is.EqualTo(name));
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
        DateTime dateTime = DateTime.UtcNow;
        DateTime createdAt = DateTime.UtcNow;
        DateTime deletedAt = DateTime.UtcNow;

        var session = Session.Create(
            userId,
            campaignId,
            name,
            dateTime,
            createdAt
        );

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
