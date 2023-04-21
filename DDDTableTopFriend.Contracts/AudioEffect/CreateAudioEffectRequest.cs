namespace DDDTableTopFriend.Contracts.Campaign;

public record CreateAudioEffectRequest(
    Guid UserId,
    string Name,
    string Description,
    string? AudioLink,
    byte[] AudioClip
);
