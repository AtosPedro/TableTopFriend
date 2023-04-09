# Domain Models

## Status

```csharp

public sealed class Status
{
    Status Create(string name, string description, float quantity);
}
```

```json
{
    "id": { "value": "00000000-0000-0000-0000-00000000000" },
    "name": "Life Points",
    "description": "represents the life energy!",
    "quantity": 0
}
```
