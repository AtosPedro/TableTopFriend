using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.AggregateCharacter.Events;

public record CharacterDeletedDomainEvent(
    CharacterId Id,
    UserId userId,
    DateTime deletedAt
) : IDomainEvent;
