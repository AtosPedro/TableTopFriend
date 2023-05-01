using System.Net.Mime;
using DDDTableTopFriend.Domain.AggregateCharacter.Entities;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.Common.Enums;
using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter.Events;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateCharacter;

public sealed class Character : AggregateRoot<CharacterId, Guid>
{
    public UserId UserId { get; set; } = null!;
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public byte[]? Image { get; private set; }
    public CharacterType Type { get; private set; }
    public CharacterSheet CharacterSheet { get; private set; } = null!;
    public IReadOnlyList<AudioEffectId> AudioEffectIds => _audioEffectIds.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<AudioEffectId> _audioEffectIds = new();

    public Character(CharacterId id) : base(id) { }

    private Character(
        CharacterId id,
        UserId userId,
        string name,
        string description,
        CharacterType type,
        List<AudioEffectId> audioEffectIds,
        DateTime createdAt) : base(id)
    {
        UserId = userId;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        Type = type;
        _audioEffectIds = audioEffectIds;
    }

    public static Character Create(
        UserId userId,
        string name,
        string description,
        CharacterType type,
        List<AudioEffectId> audioEffectIds,
        string sheetName,
        string sheetDescription,
        List<StatusId> sheetStatusIds,
        List<SkillId> sheetSkillIds,
        DateTime createdAt)
    {
        Character character = new(
            CharacterId.CreateUnique(),
            userId,
            name,
            description,
            type,
            audioEffectIds,
            createdAt
        );

        character.AddCharacterSheet(
            sheetName,
            sheetDescription,
            sheetStatusIds,
            sheetSkillIds,
            createdAt
        );

        character.AddDomainEvent(new CharacterCreatedDomainEvent(
            CharacterId.Create(character.Id.Value),
            character.UserId,
            character.Name,
            character.Description,
            character.Type,
            character.CharacterSheet,
            character.AudioEffectIds
        ));

        return character;
    }

    public void Update(
        string name,
        string description,
        CharacterType type,
        List<AudioEffectId> audioEffectIds,
        string sheetName,
        string sheetDescription,
        List<StatusId> sheetStatusIds,
        List<SkillId> sheetSkillIds,
        DateTime updatedAt)
    {
        Name = name;
        Description = description;
        Type = type;
        UpdatedAt = updatedAt;

        _audioEffectIds.AddRange(audioEffectIds.Where(c => !_audioEffectIds.Contains(c)));
        _audioEffectIds.RemoveAll(cid => _audioEffectIds.Except(audioEffectIds).Contains(cid));

        UpdateCharacterSheet(
            sheetName,
            sheetDescription,
            sheetStatusIds,
            sheetSkillIds,
            updatedAt
        );

        AddDomainEvent(new CharacterChangedDomainEvent(
            CharacterId.Create(Id.Value),
            Name,
            Description,
            Type,
            CharacterSheet,
            AudioEffectIds,
            UpdatedAt.Value
        ));
    }

    #region -- CharacterSheet --

    private void AddCharacterSheet(
        string sheetName,
        string sheetDescription,
        List<StatusId> sheetStatusIds,
        List<SkillId> sheetSkillIds,
        DateTime createdAt)
    {
        CharacterSheet = CharacterSheet.Create(
            sheetName,
            sheetDescription,
            sheetStatusIds,
            sheetSkillIds,
            createdAt
        );

        AddDomainEvent(new CharacterSheetCreatedDomainEvent(
            CharacterSheet.Name,
            CharacterSheet.Description,
            CharacterSheet.StatusIds,
            CharacterSheet.SkillIds,
            createdAt
        ));
    }

    private void UpdateCharacterSheet(
        string sheetName,
        string sheetDescription,
        List<StatusId> sheetStatusIds,
        List<SkillId> sheetSkillIds,
        DateTime updatedAt)
    {
        CharacterSheet.Update(
            sheetName,
            sheetDescription,
            sheetStatusIds,
            sheetSkillIds,
            updatedAt
        );

        AddDomainEvent(new CharacterSheetChangedDomainEvent(
            CharacterSheet.Name,
            CharacterSheet.Description,
            CharacterSheet.StatusIds,
            CharacterSheet.SkillIds,
            CharacterSheet.UpdatedAt!.Value
        ));
    }

    public void AddSkill(SkillId skillId)
    {
        CharacterSheet.AddSkillId(skillId);
    }

    public void AddStatusId(StatusId statusId)
    {
        CharacterSheet.AddStatusId(statusId);
    }

    public void MarkToDelete(DateTime deletedAt)
    {
        AddDomainEvent(new CharacterDeletedDomainEvent(
            CharacterId.Create(Id.Value),
            UserId,
            deletedAt
        ));
    }

    #endregion

#pragma warning disable CS8618
    private Character()
    {
    }
#pragma warning restore CS8618
}
