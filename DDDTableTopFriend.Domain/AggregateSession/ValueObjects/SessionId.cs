using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Common.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateSession.ValueObjects;

public sealed class SessionId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private SessionId(Guid value)
    {
        Value = value;
    }

    public static SessionId CreateUnique() => new (Guid.NewGuid());
    public static SessionId Create(Guid id) => new (id);
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
