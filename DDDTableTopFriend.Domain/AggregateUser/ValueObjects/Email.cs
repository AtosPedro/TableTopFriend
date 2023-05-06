using System.Text.RegularExpressions;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateUser.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; set; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string email)
    {
        if (!IsValid(email))
            throw new Exception();

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
