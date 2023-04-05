using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.Skill.ValueObjects;

public sealed class SkillId : ValueObject
{
    public Guid Value { get; set; }

    private SkillId(Guid value)
    {
        Value = value;
    }

    public static SkillId CreateUnique() => new (Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
