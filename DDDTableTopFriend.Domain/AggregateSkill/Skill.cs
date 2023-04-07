using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateSkill;

public class Skill : AggregateRoot<SkillId>
{
    public string Name { get; }
    public string? Description { get; }
    public float Cost { get; }
    public DateTime? CreatedAt { get; }
    public DateTime? UpdatedAt { get; }

    public Skill(
        SkillId id,
        string name,
        string? description,
        float cost,
        DateTime? createdAt,
        DateTime? updatedAt) : base(id)
    {
        Name = name;
        Description = description;
        Cost = cost;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
