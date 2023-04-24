namespace DDDTableTopFriend.Application.Characters.Common;

public record CharacterSheetDto(
    string Name,
    string Description,
    List<Guid> StatusIds,
    List<Guid> SkillIds
);
