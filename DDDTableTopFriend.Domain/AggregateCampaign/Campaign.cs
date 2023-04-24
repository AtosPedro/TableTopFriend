using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCampaign.Events;
using DDDTableTopFriend.Domain.AggregateSession.Events;

namespace DDDTableTopFriend.Domain.AggregateCampaign;

public sealed class Campaign : AggregateRoot<CampaignId, Guid>
{
    public UserId UserId { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public IReadOnlyList<CharacterId> CharacterIds => _characterIds.AsReadOnly();
    public IReadOnlyList<SessionId> SessionIds => _sessionIds.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<CharacterId> _characterIds = new();
    private readonly List<SessionId> _sessionIds = new();

    public Campaign(CampaignId id) : base(id) { }

    private Campaign(
        CampaignId id,
        UserId userId,
        string name,
        string description,
        List<CharacterId> characterIds,
        List<SessionId> sessionIds,
        DateTime createdAt) : base(id)
    {
        UserId = userId;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        _characterIds = characterIds;
        _sessionIds = sessionIds;
    }

    public static Campaign Create(
        UserId userId,
        string name,
        string description,
        List<CharacterId> characterIds,
        DateTime createdAt)
    {
        var id = CampaignId.CreateUnique();
        Campaign campaign = new(
            id,
            userId,
            name,
            description,
            characterIds ?? new List<CharacterId>(),
            new List<SessionId>(),
            createdAt
        );

        campaign.AddDomainEvent(
            new CampaignCreatedDomainEvent(
                id,
                campaign.UserId,
                campaign.Name,
                campaign.Description,
                campaign.CharacterIds,
                campaign.CreatedAt
            )
        );

        return campaign;
    }

    public void Update(
        string name,
        string description,
        List<CharacterId> characterIds,
        DateTime updatedAt)
    {
        Name = name ?? Name;
        Description = description ?? Description;
        UpdatedAt = updatedAt;
        characterIds ??= new List<CharacterId>();
        _characterIds.AddRange(characterIds.Where(c => !_characterIds.Contains(c)));
        _characterIds.RemoveAll(cid => _characterIds.Except(characterIds).Contains(cid));

        AddDomainEvent(new CampaignChangedDomainEvent(
            CampaignId.Create(Id.Value),
            UserId,
            Name,
            Description,
            CharacterIds,
            SessionIds,
            UpdatedAt.Value
        ));
    }

    public void MarkToDelete(DateTime deletedAt){
        AddDomainEvent(new CampaignDeletedDomainEvent(
            CampaignId.Create(Id.Value),
            deletedAt)
        );
    }

    public void AddCharacterId(
        CharacterId characterId,
        DateTime updatedAt)
    {
        bool exists = _characterIds.Contains(characterId);
        if (!exists)
        {
            _characterIds.Add(characterId);
            AddDomainEvent(new PlayerJoinedCampaignDomainEvent(
                UserId,
                CampaignId.Create(Id.Value),
                characterId
            ));
        }

        UpdatedAt = updatedAt;
    }

    public void AddSessionId(
        SessionId sessionId,
        DateTime updatedAt)
    {
        bool exists = _sessionIds.Contains(sessionId);
        if (!exists)
        {
            _sessionIds.Add(sessionId);
            AddDomainEvent(new SessionScheduledDomainEvent(
                UserId,
                CampaignId.Create(Id.Value),
                sessionId
            ));
        }

        UpdatedAt = updatedAt;
    }

    public void RemoveSessionId(
        SessionId sessionId,
        DateTime updatedAt)
    {
        _sessionIds.Remove(sessionId);
        UpdatedAt = updatedAt;
    }

    public void  RemoveCharacterId(
        CharacterId characterId,
        DateTime updatedAt)
    {
        _characterIds.Remove(characterId);
        UpdatedAt = updatedAt;
    }

    public bool  HasCharacter(CharacterId characterId)
    {
        return _characterIds.Contains(characterId);
    }

#pragma warning disable CS8618
    private Campaign()
    {
    }
#pragma warning restore CS8618
}
