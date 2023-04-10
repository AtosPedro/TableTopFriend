using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateCharacter.Entities;

public sealed class CharacterSheet : Entity<CharacterSheetId>
{
    public CharacterId CharacterId { get; }
    public string Name { get; }
    public string Description { get; }
    public IReadOnlyList<StatusId> StatusIds => _statusIds.AsReadOnly();
    public IReadOnlyList<SkillId> SkillIds => _skillIds.AsReadOnly();
    public DateTime? CreatedAt { get; }
    public DateTime? UpdatedAt { get; }

    private readonly List<StatusId> _statusIds = new();
    private readonly List<SkillId> _skillIds = new();

    public CharacterSheet(CharacterSheetId id) : base(id) { }

    private CharacterSheet(
        CharacterSheetId id,
        CharacterId CharacterId,
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
        this.CharacterId = CharacterId;
    }

    public static CharacterSheet Create(
        CharacterId characterId,
        string name,
        string description,
        List<StatusId> statusIds,
        List<SkillId> skillIds,
        DateTime? createdAt,
        DateTime? updatedAt)
    {
        return new(
            CharacterSheetId.CreateUnique(),
            characterId,
            name,
            description,
            statusIds,
            skillIds,
            createdAt,
            updatedAt);
    }
}
