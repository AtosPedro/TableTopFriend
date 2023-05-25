using TableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using TableTopFriend.Domain.AggregateCharacter.Entities;
using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Enums;
using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Domain.AggregateCharacter.Events;

public record CharacterCreatedDomainEvent(
    CharacterId CharacterId,
    UserId UserId,
    Name Name,
    Description Description,
    CharacterType Type,
    CharacterSheet CharacterSheet,
    IReadOnlyList<AudioEffectId> AudioEffectIds
) : IDomainEvent;
