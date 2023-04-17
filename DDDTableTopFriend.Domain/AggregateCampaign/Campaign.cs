using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCampaign.Events;
using DDDTableTopFriend.Domain.AggregateSession.Events;

namespace DDDTableTopFriend.Domain.AggregateCampaign;

public sealed class Campaign : AggregateRoot<CampaignId, Guid>
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public UserId UserId { get; private set; } = null!;
    public IReadOnlyList<CharacterId> CharacterIds => _characterIds.AsReadOnly();
    public IReadOnlyList<SessionId> SessionIds => _sessionIds.AsReadOnly();
    public DateTime? CreatedAt { get; private set; }
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
        Campaign campaign = new(
            CampaignId.CreateUnique(),
            userId,
            name,
            description,
            characterIds ?? new List<CharacterId>(),
            sessionIds ?? new List<SessionId>(),
            createdAt ?? DateTime.UtcNow,
            updatedAt
        );

        campaign.AddDomainEvent(
            new CampaignCreatedDomainEvent(
                CampaignId.Create(campaign.Id.Value),
                campaign.UserId,
                campaign.Name,
                campaign.Description,
                campaign.CharacterIds,
                campaign.SessionIds,
                campaign.CreatedAt.Value
            )
        );

        return campaign;
    }

    public static Campaign Update(
        Campaign campaign,
        DateTime? updatedAt)
    {
        campaign.UpdatedAt = updatedAt;
        campaign.AddDomainEvent(new CampaignChangedDomainEvent(
            CampaignId.Create(campaign.Id.Value),
            campaign.UserId,
            campaign.Name,
            campaign.Description,
            campaign.CharacterIds,
            campaign.SessionIds,
            campaign.CreatedAt.Value,
            campaign.UpdatedAt.Value
        ));
        return campaign;
    }

    public void AddCharacterId(CharacterId characterId)
    {
        bool exists = _characterIds.Contains(characterId);
        if (!exists)
        {
            _characterIds.Add(characterId);
            AddDomainEvent(new PlayerJoinedCampaignDomainEvent(
                CampaignId.Create(Id.Value),
                characterId
            ));
        }
    }

    public void AddSessionId(SessionId sessionId)
    {
        bool exists = _sessionIds.Contains(sessionId);
        if (!exists)
        {
            _sessionIds.Add(sessionId);
            AddDomainEvent(
                new SessionScheduledDomainEvent(
                    CampaignId.Create(Id.Value),
                    sessionId
                )
            );
        }
    }

#pragma warning disable CS8618
    private Campaign()
    {
    }
#pragma warning restore CS8618
}
