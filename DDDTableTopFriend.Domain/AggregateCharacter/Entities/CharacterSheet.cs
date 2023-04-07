using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateCharacter.Entities;

public sealed class CharacterSheet : Entity<CharacterSheetId>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IReadOnlyList<StatusId> StatusIds { get => _statusIds.AsReadOnly(); }
    public IReadOnlyList<SkillId> SkillIds { get => _skillIds.AsReadOnly(); }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    private readonly List<StatusId> _statusIds = new();
    private readonly List<SkillId> _skillIds = new();

    public CharacterSheet(
        CharacterSheetId id,
        string name,
        string description,
        List<StatusId> statusIds,
        List<SkillId> skillIds,
        DateTime? createdAt,
        DateTime? updatedAt) : base(id)
    {
        Name = name;
        Description = description;
        _statusIds = statusIds;
        _skillIds = skillIds;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static CharacterSheet Create(
        string name,
        string description,
        List<StatusId> statusIds,
        List<SkillId> skillIds,
        DateTime? createdAt,
        DateTime? updatedAt)
    {
        return new(
            CharacterSheetId.CreateUnique(),
            name,
            description,
            statusIds,
            skillIds,
            createdAt,
            updatedAt);
    }
}
