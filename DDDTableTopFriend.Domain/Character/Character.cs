using DDDTableTopFriend.Domain.Character.Entities;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Character.ValueObjects;
using DDDTableTopFriend.Domain.Common.Enums;
using DDDTableTopFriend.Domain.AudioEffect.ValueObjects;

namespace DDDTableTopFriend.Domain.Character;

public sealed class Character : AggregateRoot<CharacterId>
{
    public string Name { get; }
    public string Description { get; }
    public CharacterType Type { get; }
    public CharacterSheet CharacterSheet { get; }
    public IReadOnlyList<AudioEffectId> AudioEffectsIds { get => _audioEffectsIds.AsReadOnly(); }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    private readonly List<AudioEffectId> _audioEffectsIds = new();
    public Character(
        CharacterId id,
        string name,
        string description,
        CharacterType type,
        CharacterSheet characterSheet,
        List<AudioEffectId> audioEffectsIds,
        DateTime? createdAt,
        DateTime? updatedAt) : base(id)
    {
        Name = name;
        Description = description;
        CharacterSheet = characterSheet;
        _audioEffectsIds = audioEffectsIds;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Type = type;
    }

    public static Character Create(
        string name,
        string description,
        CharacterType type,
        CharacterSheet characterSheet,
        List<AudioEffectId> audioEffectsIds,
        DateTime? createdAt,
        DateTime? updatedAt)
    {
        return new(
            CharacterId.CreateUnique(),
            name,
            description,
            type,
            characterSheet,
            audioEffectsIds,
            createdAt,
            updatedAt);
    }
}
