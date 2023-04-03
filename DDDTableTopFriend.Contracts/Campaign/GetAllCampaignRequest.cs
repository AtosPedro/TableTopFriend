namespace DDDTableTopFriend.Contracts.Campaign;

public record GetAllCampaignRequest(
    Guid Id,
    Guid UserId
);
