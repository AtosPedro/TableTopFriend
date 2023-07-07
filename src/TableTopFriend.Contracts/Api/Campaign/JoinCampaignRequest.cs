namespace TableTopFriend.Contracts.Api.Campaign;

public record JoinCampaignRequest(
    Guid id,
    Guid characterId
);
