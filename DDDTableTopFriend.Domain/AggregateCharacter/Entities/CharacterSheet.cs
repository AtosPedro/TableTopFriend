using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateCharacter.Entities;

public sealed class CharacterSheet : Entity<CharacterSheetId>
{
    public string Name { get; private set;} = null!;
    public string Description { get; private set;} = null!;
    public IReadOnlyList<StatusId> StatusIds => _statusIds.AsReadOnly();
    public IReadOnlyList<SkillId> SkillIds => _skillIds.AsReadOnly();
    public DateTime? CreatedAt { get; private set;}
    public DateTime? UpdatedAt { get; private set;}

    private readonly List<StatusId> _statusIds = new();
    private readonly List<SkillId> _skillIds = new();

    public CharacterSheet(CharacterSheetId id) : base(id) { }

    private CharacterSheet(
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
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        _statusIds = statusIds;
        _skillIds = skillIds;
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

#pragma warning disable CS8618
    private CharacterSheet()
    {
    }
#pragma warning restore CS8618
}
