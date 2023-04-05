# Domain Models

## Character

```csharp

public sealed class Character
{
    Character Create(CharacterId id, string name, string description);
    Character Update(CharacterId id, string name, string description, IEnumerable<Guid> characterIds, IEnumerable<Guid> sessionIds);
    void AddSheet(CharacterId id, CharacterSheetId characterId);
    void AddStatus(SkillId skillId);
    void AddSkill(SkillId skillId);
}

```

```json

{
    "id":{ "value": "00000000-0000-0000-0000-00000000000" },
    "description": "character's 1 history starts ...",
    "type": "player",
    "characterSheet": {
        "id":{ "value": "00000000-0000-0000-0000-00000000000" },
        "name": "sheet 1",
        "description": "sheet 1 desc",
        "statusIds":[
            { "value": "00000000-0000-0000-0000-00000000000" },
        ],
        "skillIds":[
            { "value": "00000000-0000-0000-0000-00000000000" },
        ],
        "CreatedAt":"2023-01-01T00:00:00.0000000Z",
        "UpdatedAt":"2023-01-01T00:00:00.0000000Z",
    },
    "audioEffectsIds":[
        { "value": "00000000-0000-0000-0000-00000000000" }
    ],
    "createdAt":"2023-01-01T00:00:00.0000000Z",
    "updatedAt":"2023-01-01T00:00:00.0000000Z",
}

```
