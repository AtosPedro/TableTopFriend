# Domain Models

## Campaign

```csharp
public sealed class Campaign
{
    Campaign Create(CampaignId id, string name, string description);
    Campaign Update(CampaignId id, string name, string description, IEnumerable<CharacterId> characterIds, IEnumerable<SessionId> sessionIds);
    void AddCharacter(CampaignId id, CharacterId characterId);
}

```

```json
{
    "id": { "value": "00000000-0000-0000-0000-00000000000" },
    "userId": { "value": "00000000-0000-0000-0000-00000000000" },
    "name": "Campaign 1",
    "description": "Campaign 1 description",
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
