# Domain Models

## Character

```csharp
public sealed class Character
{
    Character Create(CharacterId id, string name, string description);
    Character Update(CharacterId id, string name, string description, IEnumerable<AudioEffectId> audioEffectIds);
    void AddSheet(CharacterSheet characterSheet);
    void AddStatus(CharacterSheet characterSheet, StatusId statusId);
    void AddSkill(CharacterSheet characterSheet, SkillId skillId);
}
```

```json
{
    "id":{ "value": "00000000-0000-0000-0000-00000000000" },
    "userId": { "value": "00000000-0000-0000-0000-00000000000" },
    "description": "character's 1 history starts ...",
    "type": 1,
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
