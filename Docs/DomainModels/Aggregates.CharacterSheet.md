# Domain models

## Character Sheet


```csharp
public sealed class CharacterSheet
{
    CharacterSheet Create(CharacterSheetId id, string name, string description);
    CharacterSheet Update(CharacterSheetId id, string name, string description, IEnumerable<Guid> statusIds, IEnumerable<Guid> skillsId);
    void AddStatus(SkillId skillId);
    void AddSkill(SkillId skillId);
    void Delete(CharacterId id);
}

```


```json
{
    "id":{ "value": "00000000-0000-0000-0000-00000000000" },
    "name": "sheet 1",
    "description": "sheet 1 desc",
    "statusIds":[
        { "value": "00000000-0000-0000-0000-00000000000" },
    ],
    "skillsIds":[
        { "value": "00000000-0000-0000-0000-00000000000" },
    ],
    "CreatedAt":"2023-01-01T00:00:00.0000000Z",
    "UpdatedAt":"2023-01-01T00:00:00.0000000Z",
}
```