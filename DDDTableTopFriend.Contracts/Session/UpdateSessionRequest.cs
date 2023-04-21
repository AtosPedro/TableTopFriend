namespace DDDTableTopFriend.Contracts.Skill;

public record UpdateSessionRequest(
    Guid UserId,
    Guid CampaignId,
    string Name,
    DateTime DateTime,
    TimeSpan Duration
);
