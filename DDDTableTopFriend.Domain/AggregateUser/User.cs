using DDDTableTopFriend.Domain.Common.Enums;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.Events;

namespace DDDTableTopFriend.Domain.AggregateUser;

public sealed class User : AggregateRoot<UserId, Guid>
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public Email Email { get; private set; }
    public Password Password { get; set; }
    public byte[]? ProfileImage { get; private set; }
    public UserRole UserRole { get; private set; }
    public Validation Validation { get; set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public User(UserId id) : base(id) { }

    private User(
        UserId id,
        string firstName,
        string lastName,
        Email email,
        Password password,
        UserRole userRole,
        Validation validation,
        DateTime createdAt) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        UserRole = userRole;
        Validation = validation;
        CreatedAt = createdAt;
    }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string plainPassword,
        string? passwordSalt,
        UserRole userRole,
        DateTime createdAt)
    {
        var id = UserId.CreateUnique();
        var user = new User(
            id,
            firstName,
            lastName,
            Email.Create(email),
            Password.CreateHashed(plainPassword, passwordSalt),
            userRole,
            Validation.Create(),
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

    public bool IsValidPassword(string plainPassword) => Password.IsValid(plainPassword);
    public void ChangePassword(string plainPassword) => Password = Password.CreateHashed(plainPassword, Password.Salt);

    public void Update(
       string firstName,
       string lastName,
       string email,
       UserRole userRole,
       DateTime updatedAt)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = Email.Create(email);
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

    public void Validate(DateTime validationDate)
    {
        Validation.Validate(validationDate);
    }

#pragma warning disable CS8618
    private User()
    {
    }
#pragma warning restore CS8618
}
