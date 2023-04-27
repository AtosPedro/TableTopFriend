using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Common.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;

public sealed class SkillId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    public SkillId(Guid value)
    {
        Value = value;
    }

    public static SkillId CreateUnique() => new (Guid.NewGuid());
    public static SkillId Create(Guid id) => new (id);
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
