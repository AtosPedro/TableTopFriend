namespace TableTopFriend.Contracts.Skill;

public record UpdateSkillRequest(
    Guid Id,
    Guid UserId,
    Guid StatusId,
    Guid AudioEffectId,
    string Name,
    string Description,
    float Cost
);
