namespace DDDTableTopFriend.Contracts.Campaign;

public record CreateCampaignRequest(
    Guid UserId,
    string Name,
    string Description,
    List<Guid> CharacterIds,
    List<Guid> SessionIds
);
