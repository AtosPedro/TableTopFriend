using DDDTableTopFriend.Domain.AggregateCampaign;
using DDDTableTopFriend.Domain.AggregateCampaign.Events;
using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSession.Events;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using NUnit.Framework;

namespace DDDTableTopFriend.Domain.Tests.AggregateCampaign;

[TestFixture]
[Author("Atos Pedro")]
public class CampaignTests
{
    [Test]
    [Author("Atos Pedro")]
    public void Create_Campaign_Should_Return_Valid_Campaign()
    {
        const string name = "";
        const string description = "";
        List<CharacterId> characterIds = new();
        UserId userId = UserId.Create(Guid.NewGuid());

        var campaign = Campaign.Create(
            userId,
            name,
            description,
            characterIds,
            DateTime.UtcNow
        );

        Assert.Multiple(() =>
        {
            Assert.That(campaign.Id, Is.Not.Null);
            Assert.That(campaign.Id.Value, Is.Not.EqualTo(Guid.Parse("00000000-0000-0000-0000-000000000000")));
            Assert.That(campaign.UserId, Is.EqualTo(userId));
            Assert.That(campaign.Name, Is.EqualTo(name));
            Assert.That(campaign.Description, Is.EqualTo(description));
            Assert.That(campaign.CharacterIds, Is.EqualTo(characterIds));
        });
    }

    [Test]
    [Author("Atos Pedro")]
    public void Update_Campaign_Should_Return_Valid_Campaign()
    {
        const string name = "campaign 1";
        const string description = "campaign 1 desc";
        List<CharacterId> characterIds = new();
        UserId userId = UserId.Create(Guid.NewGuid());

        const string nameUpdated = "campaign 1 updated";
        const string descriptionUpdated = "campaign 1 desc updated";
        List<CharacterId> characterIdsUpdated = new() { CharacterId.Create(Guid.NewGuid()) };

        var campaign = Campaign.Create(
            userId,
            name,
            description,
            characterIds,
            DateTime.UtcNow);

        campaign.Update(
            nameUpdated,
            descriptionUpdated,
            characterIdsUpdated,
            DateTime.UtcNow
        );

        Assert.Multiple(() =>
        {
            Assert.That(campaign.Id, Is.Not.Null);
            Assert.That(campaign.Id.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(campaign.UserId, Is.EqualTo(userId));
            Assert.That(campaign.Name, Is.EqualTo(nameUpdated));
            Assert.That(campaign.Description, Is.EqualTo(descriptionUpdated));
            Assert.That(campaign.CharacterIds, Is.EqualTo(characterIdsUpdated));
        });
    }

    [Test]
    [Author("Atos Pedro")]
    public void Mark_To_Delete_Should_Add_Campaign_Deleted_Domain_Event()
    {
        const string name = "";
        const string description = "";
        List<CharacterId> characterIds = new();
        UserId userId = UserId.Create(Guid.NewGuid());

        var campaign = Campaign.Create(
            userId,
            name,
            description,
            characterIds,
            DateTime.UtcNow
        );

        var deletedAt = DateTime.UtcNow;
        campaign.ClearDomainEvents();
        campaign.MarkToDelete(deletedAt);
        var deleteCampaignDomainEvent = campaign.GetDomainEvents().FirstOrDefault() as CampaignDeletedDomainEvent;

        Assert.Multiple(() =>
        {
            Assert.That(deleteCampaignDomainEvent, Is.Not.Null);
            Assert.That(deleteCampaignDomainEvent!.CampaignId, Is.EqualTo(CampaignId.Create(campaign.Id.Value)));
            Assert.That(deleteCampaignDomainEvent!.DeletedAt, Is.EqualTo(deletedAt));
        });
    }

    [Test]
    [Author("Atos Pedro")]
    public void Add_Character_Id_Should_Add_If_Id_Not_Exists()
    {
        const string name = "";
        const string description = "";
        List<CharacterId> characterIds = new();
        UserId userId = UserId.Create(Guid.NewGuid());
        CharacterId characterId = CharacterId.Create(Guid.NewGuid());
        var campaign = Campaign.Create(
            userId,
            name,
            description,
            characterIds,
            DateTime.UtcNow
        );

        var updatedAt = DateTime.UtcNow;
        campaign.AddCharacterId(
            characterId,
            updatedAt
        );

        Assert.Multiple(() =>
        {
            Assert.That(campaign.CharacterIds, Does.Contain(characterId));
            Assert.That(campaign.CharacterIds.Count(c => c == characterId), Is.EqualTo(1));
            Assert.That(campaign.UpdatedAt, Is.EqualTo(updatedAt));
        });
    }

    [Test]
    [Author("Atos Pedro")]
    public void Add__Invalid_Character_Id_Should_Not_Add()
    {
        const string name = "";
        const string description = "";
        List<CharacterId> characterIds = new();
        UserId userId = UserId.Create(Guid.NewGuid());
        CharacterId characterId = CharacterId.Create(default);
        var campaign = Campaign.Create(
            userId,
            name,
            description,
            characterIds,
            DateTime.UtcNow
        );

        var updatedAt = DateTime.UtcNow;
        campaign.AddCharacterId(
            characterId,
            updatedAt
        );

        Assert.Multiple(() =>
        {
            Assert.That(campaign.CharacterIds, Does.Not.Contain(characterId));
            Assert.That(campaign.CharacterIds.Count(c => c == characterId), Is.EqualTo(0));
            Assert.That(campaign.UpdatedAt, Is.Null);
        });
    }

    [Test]
    [Author("Atos Pedro")]
    public void Add_Character_Id_Should_Add_Player_Joined_Campaign_Domain_Event()
    {
        const string name = "";
        const string description = "";
        List<CharacterId> characterIds = new();
        UserId userId = UserId.Create(Guid.NewGuid());
        CharacterId characterId = CharacterId.Create(Guid.NewGuid());
        var campaign = Campaign.Create(
            userId,
            name,
            description,
            characterIds,
            DateTime.UtcNow
        );

        campaign.ClearDomainEvents();
        var updatedAt = DateTime.UtcNow;
        campaign.AddCharacterId(
            characterId,
            updatedAt
        );

        var playerJoinedCampaignDomainEvent = campaign.GetDomainEvents().FirstOrDefault() as PlayerJoinedCampaignDomainEvent;
        Assert.Multiple(() =>
        {
            Assert.That(playerJoinedCampaignDomainEvent, Is.Not.Null);
            Assert.That(playerJoinedCampaignDomainEvent!.CampaignId, Is.EqualTo(CampaignId.Create(campaign.Id.Value)));
            Assert.That(playerJoinedCampaignDomainEvent!.CharacterId, Is.EqualTo(characterId));
            Assert.That(playerJoinedCampaignDomainEvent!.UserId, Is.EqualTo(userId));
            Assert.That(campaign.UpdatedAt, Is.EqualTo(updatedAt));
        });
    }

    [Test]
    [Author("Atos Pedro")]
    public void Add_Session_Id_Should_Add_If_Id_Not_Exists()
    {
        const string name = "";
        const string description = "";
        List<CharacterId> characterIds = new();
        UserId userId = UserId.Create(Guid.NewGuid());
        SessionId sessionId = SessionId.Create(Guid.NewGuid());
        var campaign = Campaign.Create(
            userId,
            name,
            description,
            characterIds,
            DateTime.UtcNow
        );

        var updatedAt = DateTime.UtcNow;
        campaign.AddSessionId(
            sessionId,
            updatedAt
        );

        Assert.Multiple(() =>
        {
            Assert.That(campaign.SessionIds, Does.Contain(sessionId));
            Assert.That(campaign.SessionIds.Count(c => c == sessionId), Is.EqualTo(1));
            Assert.That(campaign.UpdatedAt, Is.EqualTo(updatedAt));
        });
    }

    [Test]
    [Author("Atos Pedro")]
    public void Add__Invalid_Session_Id_Should_Not_Add()
    {
        const string name = "";
        const string description = "";
        List<CharacterId> characterIds = new();
        UserId userId = UserId.Create(Guid.NewGuid());
        SessionId sessionId = SessionId.Create(default);
        var campaign = Campaign.Create(
            userId,
            name,
            description,
            characterIds,
            DateTime.UtcNow
        );

        var updatedAt = DateTime.UtcNow;
        campaign.AddSessionId(
            sessionId,
            updatedAt
        );

        Assert.Multiple(() =>
        {
            Assert.That(campaign.SessionIds, Does.Not.Contain(sessionId));
            Assert.That(campaign.SessionIds.Count(c => c == sessionId), Is.EqualTo(0));
            Assert.That(campaign.UpdatedAt, Is.Null);
        });
    }

    [Test]
    [Author("Atos Pedro")]
    public void Add_Session_Id_Should_Add_Session_Scheduled_Domain_Event()
    {
        const string name = "";
        const string description = "";
        List<CharacterId> characterIds = new();
        UserId userId = UserId.Create(Guid.NewGuid());
        SessionId sessionId = SessionId.Create(Guid.NewGuid());
        var campaign = Campaign.Create(
            userId,
            name,
            description,
            characterIds,
            DateTime.UtcNow
        );

        campaign.ClearDomainEvents();
        var updatedAt = DateTime.UtcNow;
        campaign.AddSessionId(
            sessionId,
            updatedAt
        );

        var sessionScheduledDomainEvent = campaign.GetDomainEvents().FirstOrDefault() as SessionScheduledDomainEvent;
        Assert.Multiple(() =>
        {
            Assert.That(sessionScheduledDomainEvent, Is.Not.Null);
            Assert.That(sessionScheduledDomainEvent!.CampaignId, Is.EqualTo(CampaignId.Create(campaign.Id.Value)));
            Assert.That(sessionScheduledDomainEvent!.SessionId, Is.EqualTo(sessionId));
            Assert.That(sessionScheduledDomainEvent!.UserId, Is.EqualTo(userId));
            Assert.That(campaign.UpdatedAt, Is.EqualTo(updatedAt));
        });
    }
}
