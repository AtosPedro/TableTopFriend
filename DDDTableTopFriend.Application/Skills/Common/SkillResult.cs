namespace DDDTableTopFriend.Application.Skills.Common;

public record SkillResult(
    Guid Id,
    Guid UserId,
    Guid StatusId,
    Guid AudioEffectId,
    string Name,
    string Description,
    float Cost,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
