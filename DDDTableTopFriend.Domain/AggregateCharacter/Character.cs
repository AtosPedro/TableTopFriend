using DDDTableTopFriend.Domain.AggregateCharacter.Entities;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.Common.Enums;
using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateCharacter;

public sealed class Character : AggregateRoot<CharacterId, Guid>
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public CharacterType Type { get; private set; }
    public CharacterSheet CharacterSheet { get; private set; } = null!;
    public IReadOnlyList<AudioEffectId> AudioEffectIds => _audioEffectIds.AsReadOnly();
    public DateTime? CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<AudioEffectId> _audioEffectIds = new();

    public Character(CharacterId id) : base(id) { }

    private Character(
        CharacterId id,
        string name,
        string description,
        CharacterType type,
        CharacterSheet characterSheet,
        List<AudioEffectId> audioEffectIds,
        DateTime? createdAt,
        DateTime? updatedAt) : base(id)
    {
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Type = type;
        CharacterSheet = characterSheet;
        _audioEffectIds = audioEffectIds;
    }

    public static Character Create(
        string name,
        string description,
        CharacterType type,
        CharacterSheet characterSheet,
        List<AudioEffectId> audioEffectIds,
        DateTime? createdAt,
        DateTime? updatedAt)
    {
        return new(
            CharacterId.CreateUnique(),
            name,
            description,
            type,
            characterSheet,
            audioEffectIds,
            createdAt,
            updatedAt
        );
    }

#pragma warning disable CS8618
    private Character()
    {
    }
#pragma warning restore CS8618
}
