# Domain Models

## Character

```csharp
public sealed class Character
{
    Character Create(
        UserId userId,
        string name,
        string description,
        CharacterType type,
        List<AudioEffectId> audioEffectIds,
        string sheetName,
        string sheetDescription,
        List<StatusId> sheetStatusIds,
        List<SkillId> sheetSkillIds,
        DateTime createdAt);

    void Update(
        string name,
        string description,
        CharacterType type,
        List<AudioEffectId> audioEffectIds,
        string sheetName,
        string sheetDescription,
        List<StatusId> sheetStatusIds,
        List<SkillId> sheetSkillIds,
        DateTime updatedAt);

    void MarkToDelete(DateTime deletedAt);

    void AddSkill(SkillId skillId);

    void AddStatusId(StatusId statusId);
}
```

```json
{
    "id":{ "value": "00000000-0000-0000-0000-00000000000" },
    "userId": { "value": "00000000-0000-0000-0000-00000000000" },
    "name": { "Value": "character 1" },
    "description":  { "Value": "character's 1 history starts ..." },
    "type": 1,
    "characterSheet": {
        "id":{ "value": "00000000-0000-0000-0000-00000000000" },
        "name":  { "Value": "sheet 1" },
        "description": { "Value": "sheet 1 desc" },
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
