namespace TableTopFriend.Contracts.Campaign;

public record UpdateAudioEffectRequest(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    string? AudioLink,
    byte[] AudioClip
);
