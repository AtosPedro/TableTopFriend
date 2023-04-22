using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.Events;

namespace DDDTableTopFriend.Domain.AggregateSkill;

public class Skill : AggregateRoot<SkillId, Guid>
{
    public UserId UserId { get; private set; } = null!;
    public StatusId StatusId { get; private set; } = null!;
    public AudioEffectId AudioEffectId { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public float Cost { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Skill(SkillId id) : base(id) { }

    private Skill(
        SkillId id,
        UserId userId,
        AudioEffectId audioEffectId,
        StatusId statusId,
        string name,
        string description,
        float cost,
        DateTime createdAt) : base(id)
    {
        UserId = userId;
        AudioEffectId = audioEffectId;
        Name = name;
        Description = description;
        Cost = cost;
        CreatedAt = createdAt;
        StatusId = statusId;
    }

    public static Skill Create(
        UserId userId,
        AudioEffectId audioEffectId,
        StatusId statusId,
        string name,
        string description,
        float cost,
        DateTime createdAt)
    {
        Skill skill = new (
            SkillId.CreateUnique(),
            userId,
            audioEffectId,
            statusId,
            name,
            description,
            cost,
            createdAt
        );

        skill.AddDomainEvent(new SkillCreatedDomainEvent(
            SkillId.Create(skill.Id.Value),
            skill.UserId,
            skill.AudioEffectId,
            skill.Name,
            skill.Description,
            skill.Cost,
            skill.CreatedAt
        ));

        return skill;
    }

    public void Update(
        AudioEffectId audioEffectId,
        string name,
        string description,
        float cost,
        DateTime updatedAt)
    {
        AudioEffectId = audioEffectId;
        Name = name;
        Description = description;
        Cost = cost;
        UpdatedAt = updatedAt;

        AddDomainEvent(new SkillChangedDomainEvent(
            SkillId.Create(Id.Value),
            UserId,
            AudioEffectId,
            Name,
            Description,
            Cost,
            UpdatedAt.Value
        ));
    }

    public void MarkToDelete(DateTime deletedAt)
    {
        AddDomainEvent(new SkillDeletedDomainEvent(
            SkillId.Create(Id.Value),
            UserId,
            deletedAt
        ));
    }

#pragma warning disable CS8618
    private Skill()
    {
    }
#pragma warning restore CS8618
}
