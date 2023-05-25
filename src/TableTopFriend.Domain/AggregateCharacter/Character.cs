using TableTopFriend.Domain.AggregateCharacter.Entities;
using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using TableTopFriend.Domain.Common.Enums;
using TableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.AggregateCharacter.Events;
using TableTopFriend.Domain.AggregateStatus.ValueObjects;
using TableTopFriend.Domain.AggregateSkill.ValueObjects;
using TableTopFriend.Domain.Common.ValueObjects;
using ErrorOr;

namespace TableTopFriend.Domain.AggregateCharacter;

public sealed class Character : AggregateRoot<CharacterId, Guid>
{
    public UserId UserId { get; set; } = null!;
    public Name Name { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
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
        Name name,
        Description description,
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

    public static ErrorOr<Character> Create(
        UserId userId,
        string nameStr,
        string descriptionStr,
        CharacterType type,
        List<AudioEffectId> audioEffectIds,
        string sheetNameStr,
        string sheetDescriptionStr,
        List<StatusId> sheetStatusIds,
        List<SkillId> sheetSkillIds,
        DateTime createdAt)
    {
        var errorList = new List<Error>();
        var name = Name.Create(nameStr);
        var description = Description.Create(descriptionStr);

        if (name.IsError)
            errorList.AddRange(name.Errors);

        if (description.IsError)
            errorList.AddRange(description.Errors);

        if (errorList.Any())
            return errorList;

        Character character = new(
            CharacterId.CreateUnique(),
            userId,
            name.Value,
            description.Value,
            type,
            audioEffectIds,
            createdAt
        );

        character.AddCharacterSheet(
            sheetNameStr,
            sheetDescriptionStr,
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

    public ErrorOr<Character> Update(
        string nameStr,
        string descriptionStr,
        CharacterType type,
        List<AudioEffectId> audioEffectIds,
        string sheetNameStr,
        string sheetDescriptionStr,
        List<StatusId> sheetStatusIds,
        List<SkillId> sheetSkillIds,
        DateTime updatedAt)
    {

        var errorList = new List<Error>();
        var name = Name.Create(nameStr);
        var description = Description.Create(descriptionStr);

        if (name.IsError)
            errorList.AddRange(name.Errors);

        if (description.IsError)
            errorList.AddRange(description.Errors);

        if (errorList.Any())
            return errorList;

        Name = name.Value ?? Name;
        Description = description.Value ?? Description;
        Type = type;
        UpdatedAt = updatedAt;

        _audioEffectIds.AddRange(audioEffectIds.Where(c => !_audioEffectIds.Contains(c)));
        _audioEffectIds.RemoveAll(cid => _audioEffectIds.Except(audioEffectIds).Contains(cid));

        UpdateCharacterSheet(
            sheetNameStr,
            sheetDescriptionStr,
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

        return this;
    }

    #region -- CharacterSheet --

    private ErrorOr<Character> AddCharacterSheet(
        string sheetName,
        string sheetDescription,
        List<StatusId> sheetStatusIds,
        List<SkillId> sheetSkillIds,
        DateTime createdAt)
    {
        var result = CharacterSheet.Create(
            sheetName,
            sheetDescription,
            sheetStatusIds,
            sheetSkillIds,
            createdAt
        );

        if (result.IsError)
            return result.Errors;

        CharacterSheet = result.Value;

        AddDomainEvent(new CharacterSheetCreatedDomainEvent(
            CharacterSheet.Name,
            CharacterSheet.Description,
            CharacterSheet.StatusIds,
            CharacterSheet.SkillIds,
            createdAt
        ));

        return this;
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
