using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;

namespace DDDTableTopFriend.Application.Sessions.Common;

public record SessionResult(
    Guid Id,
    Guid UserId,
    Guid CampaignId,
    string Name,
    DateTime DateTime,
    TimeSpan Duration,
    List<CharacterId> CharacterIds,
    List<AudioEffectId> AudioEffectIds
);
