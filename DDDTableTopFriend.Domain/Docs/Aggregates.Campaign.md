# Domain Models

## Campaign

```csharp
public sealed class Campaign
{
    Campaign Create(
        UserId userId,
        string name,
        string description,
        List<CharacterId> characterIds,
        DateTime createdAt);

    void Update(
        string name,
        string description,
        List<CharacterId> characterIds,
        DateTime updatedAt);

    void MarkToDelete(DateTime deletedAt);

    void AddSessionId(SessionId sessionId, DateTime updatedAt);

    void RemoveSessionId(SessionId sessionId, DateTime updatedAt);

    void AddCharacterId(CharacterId characterId, DateTime updatedAt);

    void RemoveCharacterId(CharacterId characterId, DateTime updatedAt);
}

```

```json
{
    "id": { "value": "00000000-0000-0000-0000-00000000000" },
    "userId": { "value": "00000000-0000-0000-0000-00000000000" },
    "name": { "Value": "Campaign 1" },
    "description": { "Value":  "Campaign 1 description" },
    "charactersIds":[
        { "value": "00000000-0000-0000-0000-00000000000" },
    ],
    "sessionIds":[
        { "value": "00000000-0000-0000-0000-00000000000" },
    ],
    "createdAt":"2023-01-01T00:00:00.0000000Z",
    "updatedAt":"2023-01-01T00:00:00.0000000Z",
}
```
