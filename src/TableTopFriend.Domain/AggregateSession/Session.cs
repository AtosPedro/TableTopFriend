using TableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using TableTopFriend.Domain.AggregateCampaign.ValueObjects;
using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.AggregateSession.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.AggregateSession.Events;
using TableTopFriend.Domain.Common.ValueObjects;
using ErrorOr;

namespace TableTopFriend.Domain.AggregateSession;

public class Session : AggregateRoot<SessionId, Guid>
{
    public UserId UserId { get; private set; } = null!;
    public CampaignId CampaignId { get; private set; } = null!;
    public Name Name { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
    public DateTime DateTime { get; private set; }
    public TimeSpan Duration { get; private set; }
    public IReadOnlyList<CharacterId> CharacterIds => _characterIds.AsReadOnly();
    public IReadOnlyList<AudioEffectId> AudioEffectIds => _audioEffectIds.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<CharacterId> _characterIds = new();
    private readonly List<AudioEffectId> _audioEffectIds = new();

    public Session(SessionId id) : base(id) { }

    private Session(
        SessionId id,
        UserId userId,
        CampaignId campaignId,
        Name name,
        Description description,
        DateTime dateTime,
        List<CharacterId> characterIds,
        List<AudioEffectId> audioEffectIds,
        DateTime createdAt) : base(id)
    {
        CampaignId = campaignId;
        UserId = userId;
        Name = name;
        Description = description;
        DateTime = dateTime;
        CreatedAt = createdAt;
        _characterIds = characterIds;
        _audioEffectIds = audioEffectIds;
    }

    public static ErrorOr<Session> Create(
        UserId userId,
        CampaignId campaignId,
        string nameStr,
        string descriptionStr,
        DateTime dateTime,
        DateTime createdAt)
    {
        var errorList = new List<Error>();
        var name = Name.Create(nameStr);
        var description = Description.Create(descriptionStr);

        if (name.IsError)
            errorList.AddRange(name.Errors);

        if (description.IsError)
            errorList.AddRange(description.Errors);

        if (errorList.Any())
            return errorList;

        var session = new Session(
            SessionId.CreateUnique(),
            userId,
            campaignId,
            name.Value,
            description.Value,
            dateTime,
            new List<CharacterId>(),
            new List<AudioEffectId>(),
            createdAt
        );

        session.AddDomainEvent(new SessionScheduledDomainEvent(
            session.UserId,
            session.CampaignId,
            SessionId.Create(session.Id.Value)
        ));

        return session;
    }

    public ErrorOr<Session> Update(
        string nameStr,
        string descriptionStr,
        DateTime dateTime,
        TimeSpan duration,
        DateTime updatedAt)
    {
        var errorList = new List<Error>();
        var name = Name.Create(nameStr);
        var description = Description.Create(descriptionStr);

        if (name.IsError)
            errorList.AddRange(name.Errors);

        if (description.IsError)
            errorList.AddRange(description.Errors);

        if (errorList.Any())
            return errorList;

        Name = name.Value ?? Name;
        Description = description.Value ?? Description;
        DateTime = dateTime;
        Duration = duration;
        UpdatedAt = updatedAt;

        AddDomainEvent(new SessionChangedDomainEvent(
            UserId,
            CampaignId,
            SessionId.Create(Id.Value)
        ));

        return this;
    }

    public void MarkToDelete(DateTime deletedAt)
    {
        AddDomainEvent(new SessionDeletedDomainEvent(
            UserId,
            SessionId.Create(Id.Value),
            CampaignId,
            deletedAt
        ));
    }

#pragma warning disable CS8618
    private Session()
    {
    }
#pragma warning restore CS8618
}
