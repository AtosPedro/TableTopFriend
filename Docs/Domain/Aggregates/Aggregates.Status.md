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
    "Id": { "value": "00000000-0000-0000-0000-00000000000" },
    "UserId": { "value": "00000000-0000-0000-0000-00000000000" },
    "Name": { "Value": "Life Points" },
    "Description": { "Value": "represents the life energy!"} ,
    "Quantity": 0,
    "CreatedAt":"2023-01-01T00:00:00.0000000Z",
    "UpdatedAt":"2023-01-01T00:00:00.0000000Z"
}
```
