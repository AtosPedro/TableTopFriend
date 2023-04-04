# Domain Models

## Campaign

```csharp
public sealed class Campaign
{
    Campaign Create(CampaignId id, string name, string description);
    Campaign Update(CampaignId id, string name, string description, IEnumerable<Guid> characterIds, IEnumerable<Guid> sessionIds);
    void Delete(CampaignId id);
    void AddCharacter(CampaignId id, CharacterId characterId);
}

```

```json
{
    "Id": { "value": "00000000-0000-0000-0000-00000000000" },
    "Name": "Campaign 1",
    "Description": "Campaign 1 description",
    "CreatedAt":"2023-01-01T00:00:00.0000000Z",
    "UpdatedAt":"2023-01-01T00:00:00.0000000Z",
    "CharactersIds":[
        { "value": "00000000-0000-0000-0000-00000000000" },
    ],
    "SessionIds":[
        { "value": "00000000-0000-0000-0000-00000000000" },
    ]
}
```
