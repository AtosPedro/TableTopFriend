using TableTopFriend.Domain.Common.Models;
using ErrorOr;

namespace TableTopFriend.Domain.Common.ValueObjects;

public sealed class Description : ValueObject
{
    private const int MAXIMUM_LENGHT = 5000;
    public string Value { get; private set; }

    private Description(string value) => Value = value;

    public static ErrorOr<Description> Create(string value)
    {
        if (value.Length > MAXIMUM_LENGHT)
            return Errors.Errors.Description.MaximumLength;

        return new Description(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return MAXIMUM_LENGHT;
    }
}
