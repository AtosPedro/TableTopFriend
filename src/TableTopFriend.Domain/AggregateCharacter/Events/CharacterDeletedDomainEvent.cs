using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.AggregateCharacter.Events;

public record CharacterDeletedDomainEvent(
    CharacterId CharacterId,
    UserId UserId,
    DateTime DeletedAt
) : IDomainEvent;
