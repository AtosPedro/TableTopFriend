using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.Session.ValueObjects;

public sealed class SessionId : ValueObject
{
    public Guid Value { get; }

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
