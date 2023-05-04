# Domain Models

## Status

```csharp

public sealed class Status
{
    Status Create(
            UserId userId,
            string name,
            string description,
            float quantity,
            DateTime createdAt);

    void Update(
            string name,
            string description,
            float quantity,
            DateTime updatedAt);

    void MarkToDelete(DateTime deletedAt);        
}
```

```json
{
    "id": { "value": "00000000-0000-0000-0000-00000000000" },
    "userId": { "value": "00000000-0000-0000-0000-00000000000" },
    "name": "Life Points",
    "description": "represents the life energy!",
    "quantity": 0,
    "createdAt":"2023-01-01T00:00:00.0000000Z",
    "updatedAt":"2023-01-01T00:00:00.0000000Z"
}
```
