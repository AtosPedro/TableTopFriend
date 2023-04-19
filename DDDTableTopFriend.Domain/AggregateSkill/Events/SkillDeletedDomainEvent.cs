using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateStatus.Events;

public record SkillDeletedDomainEvent(
    SkillId SkillId,
    UserId UserId,
    DateTime DeletedAt
) : IDomainEvent;
