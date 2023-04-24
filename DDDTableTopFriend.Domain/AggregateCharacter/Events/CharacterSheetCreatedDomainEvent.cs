using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateCharacter.Events;

public record CharacterSheetCreatedDomainEvent(
    string Name,
    string Description,
    IReadOnlyList<StatusId> StatusIds,
    IReadOnlyList<SkillId> SkillIds,
    DateTime CreatedAt
) : IDomainEvent;
