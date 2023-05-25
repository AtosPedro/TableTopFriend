namespace TableTopFriend.Contracts.Character;

public record CharacterSheetDto(
    string Name,
    string Description,
    List<Guid> StatusIds,
    List<Guid> SkillIds
);
