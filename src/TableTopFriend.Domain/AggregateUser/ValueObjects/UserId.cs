﻿using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Domain.AggregateUser.ValueObjects;

public sealed class UserId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId CreateUnique() => new(Guid.NewGuid());

    public static UserId Create(Guid id) => new(id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private UserId()
    {
    }
#pragma warning restore CS8618
}
