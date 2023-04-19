namespace DDDTableTopFriend.Contracts.Campaign;

public record UpdateAudioEffectRequest(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    List<Guid> characterIds,
    List<Guid> sessionIds
);
