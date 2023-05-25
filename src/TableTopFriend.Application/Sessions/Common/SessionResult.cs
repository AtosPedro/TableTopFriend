using TableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using TableTopFriend.Domain.AggregateCharacter.ValueObjects;

namespace TableTopFriend.Application.Sessions.Common;

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
