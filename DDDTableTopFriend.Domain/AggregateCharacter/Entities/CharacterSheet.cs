using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateCharacter.Entities;

public sealed class CharacterSheet : Entity<CharacterSheetId>
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public IReadOnlyList<StatusId> StatusIds => _statusIds.AsReadOnly();
    public IReadOnlyList<SkillId> SkillIds => _skillIds.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<StatusId> _statusIds = new();
    private readonly List<SkillId> _skillIds = new();

    public CharacterSheet(CharacterSheetId id) : base(id) { }

    private CharacterSheet(
        CharacterSheetId id,
        string name,
        string description,
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

    public static CharacterSheet Create(
        string name,
        string description,
        List<StatusId> statusIds,
        List<SkillId> skillIds,
        DateTime createdAt)
    {
        return new(
            CharacterSheetId.CreateUnique(),
            name,
            description,
            statusIds,
            skillIds,
            createdAt);
    }

    public void Update(
        string name,
        string description,
        List<StatusId> statusIds,
        List<SkillId> skillIds,
        DateTime updatedAt)
    {
        Name = name;
        Description = description;
        UpdatedAt = updatedAt;

        _statusIds.AddRange(statusIds.Where(c => !_statusIds.Contains(c)));
        _statusIds.RemoveAll(cid => _statusIds.Except(statusIds).Contains(cid));

        _skillIds.AddRange(skillIds.Where(c => !_skillIds.Contains(c)));
        _skillIds.RemoveAll(cid => _skillIds.Except(skillIds).Contains(cid));
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
