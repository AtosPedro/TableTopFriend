# Domain models

## Skill

```csharp
public sealed class Skill
{
    Skill Create(string name, string description, float cost, StatusId statusId, AudioEffectId audioEffectId);
}
```

```json
{
    "id": { "value": "00000000-0000-0000-0000-00000000000" },
    "name": "fire ball",
    "description": "launches a fireball through the hands of the caster.",
    "cost": 10,
    "statusId": { "value": "00000000-0000-0000-0000-00000000000" },
    "audioEffectId": { "value": "00000000-0000-0000-0000-00000000000" } 
}
```
