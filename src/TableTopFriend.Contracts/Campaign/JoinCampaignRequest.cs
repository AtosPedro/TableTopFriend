namespace TableTopFriend.Contracts.Campaign;

public record JoinCampaignRequest(
    Guid id,
    Guid characterId
);
