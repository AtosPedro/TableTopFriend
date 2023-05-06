using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Common.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateStatus.Events;

public record SkillCreatedDomainEvent(
    SkillId Id,
    UserId UserId,
    AudioEffectId AudioEffectId,
    StatusId StatusId,
    Name Name,
    Description Description,
    float Cost,
    DateTime CreatedAt
) : IDomainEvent;
