using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Common.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateCharacter.Events;

public record CharacterSheetCreatedDomainEvent(
    Name Name,
    Description Description,
    IReadOnlyList<StatusId> StatusIds,
    IReadOnlyList<SkillId> SkillIds,
    DateTime CreatedAt
) : IDomainEvent;
