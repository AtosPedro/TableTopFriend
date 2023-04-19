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
    public byte[]? ProfileImage { get; private set; }
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
        DateTime createdAt) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        PasswordSalt = passwordSalt;
        UserRole = userRole;
        CreatedAt = createdAt;
    }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string password,
        string passwordSalt,
        UserRole userRole,
        DateTime createdAt)
    {
        var id = UserId.CreateUnique();
        var user = new User(
            id,
            firstName,
            lastName,
            email,
            password,
            passwordSalt,
            userRole,
            createdAt);

        user.AddDomainEvent(new UserRegisteredDomainEvent(
            id,
            user.FirstName,
            user.LastName,
            user.Email,
            userRole,
            user.CreatedAt
        ));

        return user;
    }

    public void Update(
       string firstName,
       string lastName,
       string email,
       string password,
       string passwordSalt,
       UserRole userRole,
       DateTime updatedAt)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        PasswordSalt = passwordSalt;
        UserRole = userRole;
        UpdatedAt = updatedAt;

        AddDomainEvent(new UserChangedDomainEvent(
            UserId.Create(Id.Value),
            FirstName,
            LastName,
            Email,
            UserRole,
            UpdatedAt.Value
        ));
    }

    public void MarkToDelete(DateTime deletedAt)
    {
        AddDomainEvent(new DeletedUserDomainEvent(
            UserId.Create(Id.Value),
            deletedAt
        ));
    }

#pragma warning disable CS8618
    private User()
    {
    }
#pragma warning restore CS8618
}
