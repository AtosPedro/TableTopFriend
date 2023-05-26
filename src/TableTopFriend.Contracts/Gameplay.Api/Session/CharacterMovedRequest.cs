namespace TableTopFriend.Contracts.Gameplay.Api.Session;

public record CharacterMovedRequest(
    Guid userId,
    Guid characterId,
    int posX,
    int posY
);
