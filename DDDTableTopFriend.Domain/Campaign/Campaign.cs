using DDDTableTopFriend.Domain.Campaign.ValueObjects;
using DDDTableTopFriend.Domain.Character.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Session.ValueObjects;
using DDDTableTopFriend.Domain.Users.ValueObjects;

namespace DDDTableTopFriend.Domain.Campaign;

public sealed class Campaign : AggregateRoot<CampaignId>
{
    public string Name { get; }
    public string Description { get; }
    public UserId UserId { get; }
    public IReadOnlyList<CharacterId> CharacterIds => _characterIds.AsReadOnly();
    public IReadOnlyList<SessionId> SessionIds => _sessionIds.AsReadOnly();
    public DateTime? CreatedAt { get; private set;}
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<CharacterId> _characterIds = new();
    private readonly List<SessionId> _sessionIds = new();

    public Campaign(
        CampaignId id,
        UserId userId,
        string name,
        string description,
        List<CharacterId> characterIds,
        List<SessionId> sessionIds,
        DateTime? createdAt,
        DateTime? updatedAt) : base(id)
    {
        UserId = userId;
        Name = name;
        Description = description;
        _characterIds = characterIds;
        _sessionIds = sessionIds;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Campaign Create(
        UserId userId,
        string name,
        string description,
        List<CharacterId> characterIds,
        List<SessionId> sessionIds,
        DateTime? createdAt,
        DateTime? updatedAt)
    {
        return new(
        CampaignId.CreateUnique(),
        userId,
        name,
        description,
        characterIds ?? new List<CharacterId>(),
        sessionIds ?? new List<SessionId>(),
        createdAt,
        updatedAt);
    }

    public static Campaign Update(
        Campaign campaign,
        DateTime? updatedAt)
    {
        campaign.UpdatedAt = updatedAt;
        return campaign;
    }
}
