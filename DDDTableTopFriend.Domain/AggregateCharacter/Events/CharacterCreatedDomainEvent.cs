using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter.Entities;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Enums;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateCharacter.Events;

public record CharacterCreatedDomainEvent(
    UserId UserId,
    string Name,
    string Description,
    CharacterType Type,
    CharacterSheet CharacterSheet,
    IReadOnlyList<AudioEffectId> AudioEffectIds
) : IDomainEvent;
