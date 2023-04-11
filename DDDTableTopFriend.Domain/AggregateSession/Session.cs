using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateSession;

public class Session : AggregateRoot<SessionId>
{
    public CampaignId CampaignId { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public DateTime DateTime { get; private set; }
    public TimeSpan Duration { get; private set; }
    public IReadOnlyList<CharacterId> CharacterIds => _characterIds.AsReadOnly();
    public IReadOnlyList<AudioEffectId> AudioEffectIds => _audioEffectIds.AsReadOnly();
    public DateTime? CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<CharacterId> _characterIds = new();
    private readonly List<AudioEffectId> _audioEffectIds = new();

    public Session(SessionId id) : base(id) { }

    private Session(
        SessionId id,
        CampaignId campaignId,
        string name,
        DateTime dateTime,
        TimeSpan duration,
        List<CharacterId> characterIds,
        List<AudioEffectId> audioEffectIds,
        DateTime? createdAt,
        DateTime? updatedAt) : base(id)
    {
        CampaignId = campaignId;
        Name = name;
        DateTime = dateTime;
        Duration = duration;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        _characterIds = characterIds;
        _audioEffectIds = audioEffectIds;
    }

    public static Session Create(
        CampaignId campaignId,
        string name,
        DateTime dateTime,
        TimeSpan duration,
        List<CharacterId> characterIds,
        List<AudioEffectId> audioEffectIds,
        DateTime? createdAt)
    {
        return new(
            SessionId.CreateUnique(),
            campaignId,
            name,
            dateTime,
            duration,
            characterIds,
            audioEffectIds,
            createdAt,
            null
        );
    }

#pragma warning disable CS8618
    private Session()
    {
    }
#pragma warning restore CS8618
}
