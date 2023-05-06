using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.Events;
using DDDTableTopFriend.Domain.Common.ValueObjects;
using ErrorOr;

namespace DDDTableTopFriend.Domain.AggregateSkill;

public class Skill : AggregateRoot<SkillId, Guid>
{
    public UserId UserId { get; private set; } = null!;
    public StatusId StatusId { get; private set; } = null!;
    public AudioEffectId AudioEffectId { get; private set; } = null!;
    public byte[]? Image { get; private set; }
    public Name Name { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
    public float Cost { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Skill(SkillId id) : base(id) { }

    private Skill(
        SkillId id,
        UserId userId,
        AudioEffectId audioEffectId,
        StatusId statusId,
        Name name,
        Description description,
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

    public static ErrorOr<Skill> Create(
        UserId userId,
        AudioEffectId audioEffectId,
        StatusId statusId,
        string nameStr,
        string descriptionStr,
        float cost,
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

        Skill skill = new (
            SkillId.CreateUnique(),
            userId,
            audioEffectId,
            statusId,
            name.Value,
            description.Value,
            cost,
            createdAt
        );

        skill.AddDomainEvent(new SkillCreatedDomainEvent(
            SkillId.Create(skill.Id.Value),
            skill.UserId,
            skill.AudioEffectId,
            skill.StatusId,
            skill.Name,
            skill.Description,
            skill.Cost,
            skill.CreatedAt
        ));

        return skill;
    }

    public ErrorOr<Skill> Update(
        AudioEffectId audioEffectId,
        StatusId statusId,
        string nameStr,
        string descriptionStr,
        float cost,
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

        AudioEffectId = audioEffectId;
        StatusId = statusId;
        Name = name.Value;
        Description = description.Value;
        Cost = cost;
        UpdatedAt = updatedAt;

        AddDomainEvent(new SkillChangedDomainEvent(
            SkillId.Create(Id.Value),
            UserId,
            AudioEffectId,
            StatusId,
            Name,
            Description,
            Cost,
            UpdatedAt.Value
        ));

        return this;
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
