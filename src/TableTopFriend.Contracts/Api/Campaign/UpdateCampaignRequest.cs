namespace TableTopFriend.Contracts.Api.Campaign;

public record UpdateCampaignRequest(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    List<Guid> CharacterIds,
    List<Guid> SessionIds
);
