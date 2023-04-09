# Domain Models

## Audio Effects

```csharp
public sealed class AudioEffect
{
    AudioEffect Create(string name, string? description, string? audioLink, byte[]? audioClip);
}
```

```json
{
    "id": { "value": "00000000-0000-0000-0000-00000000000" },
    "name" : "fireball sound",
    "description": "fireball burning and launching sound",
    "audioLink" : "https://www.youtube.com/watch?v=FJGdoPmspiU",
    "audioClip" : "YXNkZmFzZGZhc2RmYXNkZmFzZGZhc2Rm"
}
```
