using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateStatus.Events;

public record SkillCreatedDomainEvent(
    SkillId Id,
    UserId UserId,
    AudioEffectId AudioEffectId,
    string Name,
    string Description,
    float Cost,
    DateTime CreatedAt
) : IDomainEvent;
