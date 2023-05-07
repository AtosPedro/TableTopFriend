using System.Text.RegularExpressions;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;

namespace DDDTableTopFriend.Domain.AggregateUser.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; set; }

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
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(email);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
