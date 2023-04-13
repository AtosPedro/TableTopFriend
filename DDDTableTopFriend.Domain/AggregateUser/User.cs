using DDDTableTopFriend.Domain.Common.Enums;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.Events;

namespace DDDTableTopFriend.Domain.AggregateUser;

public sealed class User : AggregateRoot<UserId, Guid>
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Password { get; private set; } = null!;
    public string PasswordSalt { get; private set; } = null!;
    public UserRole UserRole { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public User(UserId id) : base(id) { }

    private User(
        UserId id,
        string firstName,
        string lastName,
        string email,
        string password,
        string passwordSalt,
        UserRole userRole,
        DateTime? createdAt = null,
        DateTime? updatedAt = null) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        PasswordSalt = passwordSalt;
        UserRole = userRole;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string password,
        string passwordSalt,
        UserRole userRole,
        DateTime? createdAt = null)
    {
        return new(
            UserId.CreateUnique(),
            firstName,
            lastName,
            email,
            password,
            passwordSalt,
            userRole,
            createdAt,
            null);
    }

    public static User Update(
       UserId id,
       string firstName,
       string lastName,
       string email,
       string password,
       string passwordSalt,
       UserRole userRole,
       DateTime? createdAt,
       DateTime? updatedAt = null)
    {
        return new(
            id,
            firstName,
            lastName,
            email,
            password,
            passwordSalt,
            userRole,
            createdAt,
            updatedAt);
    }
#pragma warning disable CS8618
    private User()
    {
    }
#pragma warning restore CS8618
}
