using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Domain.AggregateSkill.ValueObjects;

public sealed class SkillId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private SkillId(Guid value)
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
