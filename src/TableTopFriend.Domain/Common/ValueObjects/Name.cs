using TableTopFriend.Domain.Common.Models;
using ErrorOr;

namespace TableTopFriend.Domain.Common.ValueObjects;

public sealed class Name : ValueObject
{
    private static readonly int MINIMUM_LENGHT = 2;
    private static readonly int MAXIMUM_LENGHT = 50;

    public string Value { get; private set; }

    private Name(string value) => Value = value;

    public static ErrorOr<Name> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Errors.Errors.Name.NullOrEmpty;

        if (value.Length < MINIMUM_LENGHT)
            return Errors.Errors.Name.MinimumLength;

        if (value.Length > MAXIMUM_LENGHT)
            return Errors.Errors.Name.MaximumLength;

        return new Name(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return MINIMUM_LENGHT;
        yield return MAXIMUM_LENGHT;
    }

#pragma warning disable CS8618
    private Name()
    {
    }
#pragma warning restore CS8618
}
