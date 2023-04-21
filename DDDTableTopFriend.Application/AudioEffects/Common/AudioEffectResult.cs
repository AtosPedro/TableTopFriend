namespace DDDTableTopFriend.Application.AudioEffects.Common;

public record AudioEffectResult(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    string? AudioLink,
    byte[] AudioClip,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
