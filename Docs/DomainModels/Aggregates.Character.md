# Domain Models

## Character

```csharp

public sealed class Character
{
    Character Create(CharacterId id, string name, string description);
    Character Update(CharacterId id, string name, string description, IEnumerable<Guid> characterIds, IEnumerable<Guid> sessionIds);
    void Delete(CharacterId id);
    void AddSheet(CharacterId id, CharacterSheetId characterId);
}

```

```json

{
    "id":{ "value": "00000000-0000-0000-0000-00000000000" },
    "Description": "character's 1 history starts ...",
    "CharacterSheetId": { "value": "00000000-0000-0000-0000-00000000000" },
    "audioEffectsIds":[
        { "value": "00000000-0000-0000-0000-00000000000" }
    ],
    "CreatedAt":"2023-01-01T00:00:00.0000000Z",
    "UpdatedAt":"2023-01-01T00:00:00.0000000Z",
}

```