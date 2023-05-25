using TableTopFriend.Domain.AggregateSkill.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.AggregateStatus.Events;

public record SkillDeletedDomainEvent(
    SkillId SkillId,
    UserId UserId,
    DateTime DeletedAt
) : IDomainEvent;
