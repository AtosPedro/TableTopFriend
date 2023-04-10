using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateSession;

public class Session : AggregateRoot<SessionId>
{
    public CampaignId CampaignId { get; }
    public string Name { get; }
    public DateTime DateTime { get; }
    public TimeSpan Duration { get; }
    public IReadOnlyList<CharacterId> CharacterIds => _characterIds.AsReadOnly();
    public IReadOnlyList<AudioEffectId> AudioEffectIds => _audioEffectIds.AsReadOnly();
    public DateTime? CreatedAt { get; }
    public DateTime? UpdatedAt { get; }
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
}
