using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateSkill;

public class Skill : AggregateRoot<SkillId>
{
    public StatusId StatusId { get; private set; } = null!;
    public AudioEffectId AudioEffectId { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public float Cost { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Skill(SkillId id) : base(id) { }

    private Skill(
        SkillId id,
        AudioEffectId audioEffectId,
        string name,
        string description,
        float cost,
        DateTime? createdAt,
        DateTime? updatedAt) : base(id)
    {
        AudioEffectId = audioEffectId;
        Name = name;
        Description = description;
        Cost = cost;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Skill Create(
        AudioEffectId audioEffectId,
        string name,
        string description,
        float cost,
        DateTime? createdAt)
    {
        return new(
            SkillId.CreateUnique(),
            audioEffectId,
            name,
            description,
            cost,
            createdAt,
            null
        );
    }

#pragma warning disable CS8618
    private Skill()
    {
    }
#pragma warning restore CS8618
}
