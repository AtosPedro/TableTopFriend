namespace DDDTableTopFriend.Contracts.Character;

public record UpdateCharacterRequest(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    int Type,
    CharacterSheetDto CharacterSheet,
    List<Guid> AudioEffectIds
);
