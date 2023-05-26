namespace TableTopFriend.Contracts.Gameplay.Api.Session;

public record CharacterCastedSpellRequest(
    Guid userId,
    Guid characterId,
    Guid targetId,
    Guid spellId
);
