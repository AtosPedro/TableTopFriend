using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter.Entities;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.Common.Enums;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Common.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateCharacter.Events;

public record CharacterChangedDomainEvent(
    CharacterId CharacterId,
    Name Name,
    Description Description,
    CharacterType Type,
    CharacterSheet CharacterSheet,
    IReadOnlyList<AudioEffectId> AudioEffectIds,
    DateTime updatedAt
) : IDomainEvent;
