using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Common.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateCharacter.Events;

public record CharacterSheetChangedDomainEvent(
    Name name,
    Description description,
    IReadOnlyList<StatusId> statusIds,
    IReadOnlyList<SkillId> skillIds,
    DateTime updatedAt
) : IDomainEvent;
