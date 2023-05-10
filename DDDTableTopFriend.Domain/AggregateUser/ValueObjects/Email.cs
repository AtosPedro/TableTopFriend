using System.Text.RegularExpressions;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;

namespace DDDTableTopFriend.Domain.AggregateUser.ValueObjects;

public sealed class Email : ValueObject
{
    static Regex regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    public string Value { get; private set; }

    private Email(string value)
    {
        Value = value;
    }

    public static ErrorOr<Email> Create(string email)
    {
        if (string.IsNullOrEmpty(email))
            return Errors.Email.NullOrEmpty;

        if (!IsValid(email))
            return Errors.Email.InvalidEmail;

        return new Email(email);
    }

    public static bool IsValid(string email)
    {
        return regex.IsMatch(email);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private Email()
    {
    }
#pragma warning restore CS8618
}
