# Domain Models

## Session

```csharp
public sealed class Session
{
    Session Create(
            UserId userId,
            CampaignId campaignId,
            string name,
            DateTime dateTime,
            DateTime createdAt);

    void Update(
        string name,
        DateTime dateTime,
        TimeSpan duration,
        DateTime updatedAt);

    void MarkToDelete(DateTime deletedAt);
}
```

```json
{
    "userId": { "value": "00000000-0000-0000-0000-00000000000" },
    "campaignId": { "value": "00000000-0000-0000-0000-00000000000" },
    "name":"name",
    "dateTime":"2023-01-01T00:00:00.0000000Z",
    "duration":"00:00:00",
    "characterIds" :[
        { "value" : "00000000-0000-0000-0000-00000000000" }
    ],
    "audioEffectIds" :[
        { "value" : "00000000-0000-0000-0000-00000000000" }
    ],
    "createdAt":"2023-01-01T00:00:00.0000000Z",
    "updatedAt":"2023-01-01T00:00:00.0000000Z"
}
```
