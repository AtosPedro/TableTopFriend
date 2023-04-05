using DDDTableTopFriend.Domain.Campaign.ValueObjects;
using DDDTableTopFriend.Domain.Character.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Session.ValueObjects;

namespace DDDTableTopFriend.Domain.Campaign;

public sealed class Campaign : AggregateRoot<CampaignId>
{
    public string Name { get; }
    public string Description { get; }
    public IReadOnlyList<CharacterId> CharacterIds => _characterIds.AsReadOnly();
    public IReadOnlyList<SessionId> SessionIds => _sessionIds.AsReadOnly();
    public DateTime? CreatedAt { get; }
    public DateTime? UpdatedAt { get; }

    private readonly List<CharacterId> _characterIds = new();
    private readonly List<SessionId> _sessionIds = new();

    public Campaign(
        CampaignId id,
        string name,
        string description,
        List<CharacterId> characterIds,
        List<SessionId> sessionIds,
        DateTime? createdAt,
        DateTime? updatedAt) : base(id)
    {
        Name = name;
        Description = description;
        _characterIds = characterIds;
        _sessionIds = sessionIds;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Campaign Create(
        string name,
        string description,
        List<CharacterId> characterIds,
        List<SessionId> sessionIds,
        DateTime? createdAt,
        DateTime? updatedAt)
    {
        return new(
        CampaignId.CreateUnique(),
        name,
        description,
        characterIds,
        sessionIds,
        createdAt,
        updatedAt);
    }
}
