namespace DDDTableTopFriend.Contracts.Campaign;

public record UpdateCampaignRequest(
    Guid id,
    string Name,
    string Description,
    List<Guid> characterIds,
    List<Guid> sessionIds
);
