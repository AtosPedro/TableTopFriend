using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateUser.ValueObjects;

public sealed class UserId : ValueObject
{
    public Guid Value { get; set; }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId CreateUnique() => new (Guid.NewGuid());
    public static UserId Create(Guid id) => new (id);
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
