using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Common.Services;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;

namespace DDDTableTopFriend.Domain.AggregateUser.ValueObjects;

public sealed class Password : ValueObject
{
    public string Value { get; }
    public string Salt { get; }

    private Password(
        string value,
        string salt)
    {
        Value = value;
        Salt = salt;
    }

    public static ErrorOr<Password> CreateHashed(string plainPassword, string? salt)
    {
        if (plainPassword.Length < 8)
            return Errors.Password.MinimumLength;

        salt ??= Hasher.GenerateSalt();
        string hashedPassword = Hasher.ComputeHash(
            plainPassword,
            salt,
            1000);

        return new Password(hashedPassword, salt);
    }

    public static Password Create(string hashedPassword, string salt) => new(hashedPassword, salt);

    public bool IsValid(string plainPassword)
    {
        string hashedPassword = Hasher.ComputeHash(
            plainPassword,
            Salt,
            1000);

        return Value == hashedPassword;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Salt;
    }

#pragma warning disable CS8618
    private Password()
    {
    }
#pragma warning restore CS8618
}
