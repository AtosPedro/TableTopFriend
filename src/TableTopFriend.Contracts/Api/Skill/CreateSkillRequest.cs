namespace TableTopFriend.Contracts.Api.Skill;

public record CreateSkillRequest(
    Guid UserId,
    Guid StatusId,
    Guid AudioEffectId,
    string Name,
    string Description,
    float Cost
);
