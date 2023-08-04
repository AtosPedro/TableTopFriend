using TableTopFriend.Domain.Common.Models;
using ErrorOr;

namespace TableTopFriend.Domain.Common.ValueObjects;

public sealed class Description : ValueObject
{
    private const int MaximumLength = 5000;
    public string Value { get; private set; }

    private Description(string value) => Value = value;

    public static ErrorOr<Description> Create(string value)
    {
        if (value.Length > MaximumLength)
            return Errors.Errors.Description.MaximumLength;

        return new Description(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return MaximumLength;
    }

#pragma warning disable CS8618
    private Description()
    {
    }
#pragma warning restore CS8618
}
