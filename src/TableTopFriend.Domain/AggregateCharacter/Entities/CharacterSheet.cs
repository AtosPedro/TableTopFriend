using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.AggregateSkill.ValueObjects;
using TableTopFriend.Domain.AggregateStatus.ValueObjects;
using TableTopFriend.Domain.Common.ValueObjects;
using ErrorOr;

namespace TableTopFriend.Domain.AggregateCharacter.Entities;

public sealed class CharacterSheet : Entity<CharacterSheetId>
{
    public Name Name { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
    public IReadOnlyList<StatusId> StatusIds => _statusIds.AsReadOnly();
    public IReadOnlyList<SkillId> SkillIds => _skillIds.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<StatusId> _statusIds = new();
    private readonly List<SkillId> _skillIds = new();

    public CharacterSheet(CharacterSheetId id) : base(id) { }

    private CharacterSheet(
        CharacterSheetId id,
        Name name,
        Description description,
        List<StatusId> statusIds,
        List<SkillId> skillIds,
        DateTime createdAt) : base(id)
    {
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        _statusIds = statusIds;
        _skillIds = skillIds;
    }

    public static ErrorOr<CharacterSheet> Create(
        string nameStr,
        string descriptionStr,
        List<StatusId> statusIds,
        List<SkillId> skillIds,
        DateTime createdAt)
    {
        var errorList = new List<Error>();
        var name = Name.Create(nameStr);
        var description = Description.Create(descriptionStr);

        if (name.IsError)
            errorList.AddRange(name.Errors);

        if (description.IsError)
            errorList.AddRange(description.Errors);

        if(errorList.Any())
            return errorList;

        return new CharacterSheet(
            CharacterSheetId.CreateUnique(),
            name.Value,
            description.Value,
            statusIds,
            skillIds,
            createdAt);
    }

    public ErrorOr<CharacterSheet> Update(
        string nameStr,
        string descriptionStr,
        List<StatusId> statusIds,
        List<SkillId> skillIds,
        DateTime updatedAt)
    {
        var errorList = new List<Error>();
        var name = Name.Create(nameStr);
        var description = Description.Create(descriptionStr);

        if (name.IsError)
            errorList.AddRange(name.Errors);

        if (description.IsError)
            errorList.AddRange(description.Errors);

        if(errorList.Any())
            return errorList;

        Name = name.Value ?? Name;
        Description = description.Value ?? Description;
        UpdatedAt = updatedAt;

        _statusIds.AddRange(statusIds.Where(c => !_statusIds.Contains(c)));
        _statusIds.RemoveAll(cid => _statusIds.Except(statusIds).Contains(cid));

        _skillIds.AddRange(skillIds.Where(c => !_skillIds.Contains(c)));
        _skillIds.RemoveAll(cid => _skillIds.Except(skillIds).Contains(cid));

        return this;
    }

    public void AddStatusId(StatusId statusId)
    {
        bool exists = _statusIds.Contains(statusId);
        if (!exists)
            _statusIds.Add(statusId);
    }

    public void AddSkillId(SkillId skillId)
    {
        bool exists = _skillIds.Contains(skillId);
        if (!exists)
            _skillIds.Add(skillId);
    }

#pragma warning disable CS8618
    private CharacterSheet()
    {
    }
#pragma warning restore CS8618
}
