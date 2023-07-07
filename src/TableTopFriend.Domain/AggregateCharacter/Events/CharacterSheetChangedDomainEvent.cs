using TableTopFriend.Domain.AggregateSkill.ValueObjects;
using TableTopFriend.Domain.AggregateStatus.ValueObjects;
using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Domain.AggregateCharacter.Events;

public record CharacterSheetChangedDomainEvent(
    Name name,
    Description description,
    IReadOnlyList<StatusId> statusIds,
    IReadOnlyList<SkillId> skillIds,
    DateTime updatedAt
) : IDomainEvent;
