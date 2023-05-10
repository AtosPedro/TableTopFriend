# Domain models

## Skill

```csharp
public sealed class Skill
{
    Skill Create(
            UserId userId,
            AudioEffectId audioEffectId,
            StatusId statusId,
            string name,
            string description,
            float cost,
            DateTime createdAt);

    Update(
        AudioEffectId audioEffectId,
        string name,
        string description,
        float cost,
        DateTime updatedAt);

    void MarkToDelete(DateTime deletedAt);
}
```

```json
{
    "id": { "value": "00000000-0000-0000-0000-00000000000" },
    "userId": { "value": "00000000-0000-0000-0000-00000000000" },
    "statusId": { "value": "00000000-0000-0000-0000-00000000000" },
    "audioEffectId": { "value": "00000000-0000-0000-0000-00000000000" } ,
    "name": { "Value": "fire ball"},
    "description": { "Value": "launches a fireball through the hands of the caster."},
    "cost": 10,
    "createdAt":"2023-01-01T00:00:00.0000000Z",
    "updatedAt":"2023-01-01T00:00:00.0000000Z",
}
```
