namespace TableTopFriend.Contracts.Character;

public record CreateCharacterRequest(
    Guid UserId,
    string Name,
    string Description,
    int Type,
    CharacterSheetDto CharacterSheet,
    List<Guid> AudioEffectIds
);
