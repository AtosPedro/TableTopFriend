namespace TableTopFriend.Contracts.Api.Character;

public record CharacterSheetDto(
    string Name,
    string Description,
    List<Guid> StatusIds,
    List<Guid> SkillIds
);
