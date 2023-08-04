using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Domain.AggregateSession.ValueObjects;

public sealed class SessionId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private SessionId(Guid value)
    {
        Value = value;
    }

    public static SessionId CreateUnique() => new(Guid.NewGuid());
    public static SessionId Create(Guid id) => new(id);
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private SessionId()
    {
    }
#pragma warning restore CS8618
}
