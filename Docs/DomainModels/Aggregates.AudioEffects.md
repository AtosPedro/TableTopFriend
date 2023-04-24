# Domain Models

## Audio Effects

```csharp
public sealed class AudioEffect
{
    AudioEffect Create(
        UserId userId,
        string name,
        string? description,
        string? audioLink,
        byte[]? audioClip,
        DateTime createdAt);

    void Update(
        string name,
        string? description,
        string? audioLink,
        byte[]? audioClip,
        DateTime updatedAt);
}
```

```json
{
    "id": { "value": "00000000-0000-0000-0000-00000000000" },
    "userId": { "value": "00000000-0000-0000-0000-00000000000" },
    "name" : "fireball sound",
    "description": "fireball burning and launching sound",
    "audioLink" : "https://www.youtube.com/watch?v=FJGdoPmspiU",
    "audioClip" : "YXNkZmFzZGZhc2RmYXNkZmFzZGZhc2Rm",
    "createdAt":"2023-01-01T00:00:00.0000000Z",
    "updatedAt":"2023-01-01T00:00:00.0000000Z"
}
```
