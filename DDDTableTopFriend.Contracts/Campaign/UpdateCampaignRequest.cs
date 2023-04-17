namespace DDDTableTopFriend.Contracts.Campaign;

public record UpdateCampaignRequest(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    List<Guid> characterIds,
    List<Guid> sessionIds
);
