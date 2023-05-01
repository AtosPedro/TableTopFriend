using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateStatus.Events;

public record SkillChangedDomainEvent(
    SkillId Id,
    UserId UserId,
    AudioEffectId AudioEffectId,
    StatusId StatusId,
    string Name,
    string Description,
    float Cost,
    DateTime UpdatedAt
) : IDomainEvent;
