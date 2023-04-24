namespace DDDTableTopFriend.Application.Characters.Common;

public record CharacterResult(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    int Type,
    CharacterSheetResult CharacterSheet,
    IReadOnlyList<Guid> AudioEffectIds,
    DateTime CreatedAt,
    DateTime? UpdateAt
);
