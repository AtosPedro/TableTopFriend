namespace DDDTableTopFriend.Contracts.Campaign;

public record UpdateCampaignRequest(
    Guid Id,
    string Name,
    string Description
);
