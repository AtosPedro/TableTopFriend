﻿using DDDTableTopFriend.Domain.Common.Models;
using ErrorOr;

namespace DDDTableTopFriend.Domain.Common.ValueObjects;

public sealed class Name : ValueObject
{
    private const int MinimumLength = 2;
    private const int MaximumLength = 50;

    public string Value { get; }

    private Name(string value) => Value = value;

    public static ErrorOr<Name> Create (string value)
    {
        if (string.IsNullOrEmpty(value))
            return Errors.Errors.Name.NullOrEmpty;

        if (value.Length < MinimumLength)
            return Errors.Errors.Name.MinimumLength;

        if (value.Length < MaximumLength)
            return Errors.Errors.Name.MaximumLength;

        return new Name(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
