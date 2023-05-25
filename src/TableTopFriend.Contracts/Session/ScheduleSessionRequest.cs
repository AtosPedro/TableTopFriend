namespace TableTopFriend.Contracts.Skill;

public record ScheduleSessionRequest(
    Guid UserId,
    Guid CampaignId,
    string Name,
    DateTime DateTime
);
