using TableTopFriend.Domain.AggregateSkill.ValueObjects;
using TableTopFriend.Domain.AggregateStatus.ValueObjects;
using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Domain.AggregateCharacter.Events;

public record CharacterSheetCreatedDomainEvent(
    Name Name,
    Description Description,
    IReadOnlyList<StatusId> StatusIds,
    IReadOnlyList<SkillId> SkillIds,
    DateTime CreatedAt
) : IDomainEvent;
