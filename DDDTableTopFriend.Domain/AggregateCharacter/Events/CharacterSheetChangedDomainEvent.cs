using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateCharacter.Events;

public record CharacterSheetChangedDomainEvent(
    string name,
    string description,
    IReadOnlyList<StatusId> statusIds,
    IReadOnlyList<SkillId> skillIds,
    DateTime updatedAt
) : IDomainEvent;
