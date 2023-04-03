namespace DDDTableTopFriend.Application.Campaigns.Common;

public record CampaignResult(
    Guid Id,
    string Name,
    string Description,
    List<CampaignCharacterResult> Characters
);
