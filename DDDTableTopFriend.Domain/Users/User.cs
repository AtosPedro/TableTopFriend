using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Users.ValueObjects;

namespace DDDTableTopFriend.Domain.Entities;

public sealed class User : AggregateRoot<UserId>
{
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string Password { get; }
    public string PasswordSalt { get; }

    public User(
        UserId id,
        string firstName,
        string lastName,
        string email,
        string password,
        string passwordSalt) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        PasswordSalt = passwordSalt;
    }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string password,
        string passwordSalt)
    {
        return new(
            UserId.CreateUnique(),
            firstName,
            lastName,
            email,
            password,
            passwordSalt);
    }

    public static User Update(
       UserId id,
       string firstName,
       string lastName,
       string email,
       string password,
       string passwordSalt)
    {
        return new(
            id,
            firstName,
            lastName,
            email,
            password,
            passwordSalt);
    }
}
