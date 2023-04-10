using DDDTableTopFriend.Domain.AggregateCharacter.Entities;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.Common.Enums;
using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateCharacter;

public sealed class Character : AggregateRoot<CharacterId>
{
    public string Name { get; }
    public string Description { get; }
    public CharacterType Type { get; }
    public CharacterSheet CharacterSheet { get; }
    public IReadOnlyList<AudioEffectId> AudioEffectIds => _audioEffectIds.AsReadOnly();
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    private readonly List<AudioEffectId> _audioEffectIds = new();

    public Character(CharacterId id):base(id){}

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
        CharacterSheet = characterSheet;
        _audioEffectIds = audioEffectIds;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Type = type;
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
            updatedAt);
    }
}
