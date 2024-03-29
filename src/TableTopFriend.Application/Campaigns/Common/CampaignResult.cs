namespace TableTopFriend.Application.Campaigns.Common;

public record CampaignResult(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    List<Guid> CharacterIds,
    List<Guid> SessionIds,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
