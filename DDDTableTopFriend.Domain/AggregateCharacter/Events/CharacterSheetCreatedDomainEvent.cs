using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateCharacter.Events;

public record CharacterSheetCreatedDomainEvent(
) : IDomainEvent;
