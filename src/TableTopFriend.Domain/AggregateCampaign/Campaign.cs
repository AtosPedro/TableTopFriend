using TableTopFriend.Domain.AggregateCampaign.ValueObjects;
using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.AggregateSession.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.AggregateCampaign.Events;
using TableTopFriend.Domain.AggregateSession.Events;
using TableTopFriend.Domain.Common.ValueObjects;
using TableTopFriend.Domain.Common.Errors;
using ErrorOr;

namespace TableTopFriend.Domain.AggregateCampaign;

public sealed class Campaign : AggregateRoot<CampaignId, Guid>
{
    public UserId UserId { get; private set; } = null!;
    public Name Name { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
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
        Name name,
        Description description,
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

    public static ErrorOr<Campaign> Create(
        UserId userId,
        string nameStr,
        string descriptionStr,
        List<CharacterId> characterIds,
        DateTime createdAt)
    {
        if (userId is null)
            return Errors.User.InvalidId;

        CampaignId id = CampaignId.CreateUnique();
        List<Error> errors = new();
        ErrorOr<Name> name = Name.Create(nameStr);
        ErrorOr<Description> description = Description.Create(descriptionStr);

        if (name.IsError)
            errors.AddRange(name.Errors);

        if (description.IsError)
            errors.AddRange(description.Errors);

        if (errors.Any())
            return errors;

        Campaign campaign = new(
            id,
            userId,
            name.Value,
            description.Value,
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

    public ErrorOr<Campaign> Update(
        string nameStr,
        string descriptionStr,
        List<CharacterId> characterIds,
        DateTime updatedAt)
    {
        var errors = new List<Error>();
        var name = Name.Create(nameStr);
        var description = Description.Create(descriptionStr);

        if (name.IsError)
            errors.AddRange(name.Errors);

        if (description.IsError)
            errors.AddRange(description.Errors);

        if (errors.Any())
            return errors;

        Name = name.Value ?? Name;
        Description = description.Value ?? Description;
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

        return this;
    }

    public void MarkToDelete(DateTime deletedAt)
    {
        AddDomainEvent(new CampaignDeletedDomainEvent(
            CampaignId.Create(Id.Value),
            deletedAt)
        );
    }

    public void AddCharacterId(
        CharacterId characterId,
        DateTime updatedAt)
    {
        if (characterId.Value == default)
            return;

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
        if (sessionId.Value == default)
            return;

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

    public void RemoveCharacterId(
        CharacterId characterId,
        DateTime updatedAt)
    {
        _characterIds.Remove(characterId);
        UpdatedAt = updatedAt;
    }

    public bool HasCharacter(CharacterId characterId)
    {
        return _characterIds.Contains(characterId);
    }

#pragma warning disable CS8618
    private Campaign()
    {
    }
#pragma warning restore CS8618
}
