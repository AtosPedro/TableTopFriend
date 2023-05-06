using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;

namespace DDDTableTopFriend.Application.Sessions.Common;

public record SessionResult(
    Guid Id,
    Guid UserId,
    Guid CampaignId,
    string Name,
    string Description,
    DateTime DateTime,
    TimeSpan Duration,
    List<Guid> CharacterIds,
    List<Guid> AudioEffectIds
);
