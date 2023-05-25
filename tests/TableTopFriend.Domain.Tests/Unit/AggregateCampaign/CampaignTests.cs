using System.Reflection.Metadata.Ecma335;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Domain.AggregateCampaign;
using TableTopFriend.Domain.AggregateCampaign.Events;
using TableTopFriend.Domain.AggregateCampaign.ValueObjects;
using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using TableTopFriend.Domain.AggregateSession.Events;
using TableTopFriend.Domain.AggregateSession.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.ValueObjects;
using Moq;
using NUnit.Framework;

namespace TableTopFriend.Domain.Tests.AggregateCampaign;

[TestFixture]
[Author("Atos Pedro")]
public class CampaignTests
{
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock = new();
    private readonly IDateTimeProvider _dateTimeProvider;

    public CampaignTests()
    {
        var mockDate = DateTime.Parse("06/05/2023 00:00:00");
        _dateTimeProviderMock.Setup(x => x.UtcNow).Returns(
            mockDate
        );
        _dateTimeProvider = _dateTimeProviderMock.Object;
    }

    [Test]
    [Author("Atos Pedro")]
    public void Create_Campaign_Should_Return_Valid_Campaign()
    {
        const string name = "test name";
        const string description = "test description";
        List<CharacterId> characterIds = new();
        UserId userId = UserId.CreateUnique();

        Name nameVo = Name.Create(name).Value;
        Description descriptionVo = Description.Create(description).Value;

        var campaign = Campaign.Create(
            userId,
            name,
            description,
            characterIds,
            _dateTimeProvider.UtcNow
        ).Value;

        Assert.Multiple(() =>
        {
            Assert.That(campaign.Id, Is.Not.Null);
            Assert.That(campaign.Id.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(campaign.UserId, Is.EqualTo(userId));
            Assert.That(campaign.Name, Is.EqualTo(nameVo));
            Assert.That(campaign.Description, Is.EqualTo(descriptionVo));
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
        UserId userId = UserId.CreateUnique();

        const string nameUpdated = "campaign 1 updated";
        const string descriptionUpdated = "campaign 1 desc updated";
        List<CharacterId> characterIdsUpdated = new() { CharacterId.CreateUnique() };

        Name nameUpdatedVo = Name.Create(nameUpdated).Value;
        Description descriptionUpdatedVo = Description.Create(descriptionUpdated).Value;

        var campaign = Campaign.Create(
            userId,
            name,
            description,
            characterIds,
            _dateTimeProvider.UtcNow).Value;

        campaign.Update(
            nameUpdated,
            descriptionUpdated,
            characterIdsUpdated,
            _dateTimeProvider.UtcNow
        );

        Assert.Multiple(() =>
        {
            Assert.That(campaign.Id, Is.Not.Null);
            Assert.That(campaign.Id.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(campaign.UserId, Is.EqualTo(userId));
            Assert.That(campaign.Name, Is.EqualTo(nameUpdatedVo));
            Assert.That(campaign.Description, Is.EqualTo(descriptionUpdatedVo));
            Assert.That(campaign.CharacterIds, Is.EqualTo(characterIdsUpdated));
        });
    }

    [Test]
    [Author("Atos Pedro")]
    public void Mark_To_Delete_Should_Add_Campaign_Deleted_Domain_Event()
    {
        const string name = "campaign 1";
        const string description = "campaign 1 desc";
        List<CharacterId> characterIds = new();
        UserId userId = UserId.CreateUnique();

        var campaign = Campaign.Create(
            userId,
            name,
            description,
            characterIds,
            _dateTimeProvider.UtcNow
        ).Value;

        var deletedAt = _dateTimeProvider.UtcNow;
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
        const string name = "campaign 1";
        const string description = "campaign 1 desc";
        List<CharacterId> characterIds = new();
        UserId userId = UserId.CreateUnique();
        CharacterId characterId = CharacterId.CreateUnique();
        var campaign = Campaign.Create(
            userId,
            name,
            description,
            characterIds,
            _dateTimeProvider.UtcNow
        ).Value;

        var updatedAt = _dateTimeProvider.UtcNow;
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
        const string name = "campaign 1";
        const string description = "campaign 1 desc";
        List<CharacterId> characterIds = new();
        UserId userId = UserId.Create(Guid.NewGuid());
        CharacterId characterId = CharacterId.Create(default);
        var campaign = Campaign.Create(
            userId,
            name,
            description,
            characterIds,
            _dateTimeProvider.UtcNow
        ).Value;

        var updatedAt = _dateTimeProvider.UtcNow;
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
        const string name = "campaign 1";
        const string description = "campaign 1 desc";
        List<CharacterId> characterIds = new();
        UserId userId = UserId.Create(Guid.NewGuid());
        CharacterId characterId = CharacterId.Create(Guid.NewGuid());
        var campaign = Campaign.Create(
            userId,
            name,
            description,
            characterIds,
            _dateTimeProvider.UtcNow
        ).Value;

        campaign.ClearDomainEvents();
        var updatedAt = _dateTimeProvider.UtcNow;
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
        const string name = "campaign 1";
        const string description = "campaign 1 desc";
        List<CharacterId> characterIds = new();
        UserId userId = UserId.Create(Guid.NewGuid());
        SessionId sessionId = SessionId.Create(Guid.NewGuid());
        var campaign = Campaign.Create(
            userId,
            name,
            description,
            characterIds,
            _dateTimeProvider.UtcNow
        ).Value;

        var updatedAt = _dateTimeProvider.UtcNow;
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
    public void Add_Invalid_Session_Id_Should_Not_Add()
    {
        const string name = "campaign 1";
        const string description = "campaign 1 desc";
        List<CharacterId> characterIds = new();
        UserId userId = UserId.Create(Guid.NewGuid());
        SessionId sessionId = SessionId.Create(default);
        var campaign = Campaign.Create(
            userId,
            name,
            description,
            characterIds,
            _dateTimeProvider.UtcNow
        ).Value;

        var updatedAt = _dateTimeProvider.UtcNow;
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
        const string name = "campaign 1";
        const string description = "campaign 1 desc";
        List<CharacterId> characterIds = new();
        UserId userId = UserId.Create(Guid.NewGuid());
        SessionId sessionId = SessionId.Create(Guid.NewGuid());
        var campaign = Campaign.Create(
            userId,
            name,
            description,
            characterIds,
            _dateTimeProvider.UtcNow
        ).Value;

        campaign.ClearDomainEvents();
        var updatedAt = _dateTimeProvider.UtcNow;
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
