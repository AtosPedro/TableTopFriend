namespace DDDTableTopFriend.Application.Characters.Common;

public record CharacterSheetResult(
    string Name,
    string Description,
    IReadOnlyList<Guid> StatusIds,
    IReadOnlyList<Guid> SkillIds,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
