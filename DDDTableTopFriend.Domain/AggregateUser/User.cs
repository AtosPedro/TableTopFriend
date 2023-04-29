using DDDTableTopFriend.Domain.Common.Enums;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.Events;
using DDDTableTopFriend.Domain.Common.Services;

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
    public UserValidation Validation { get; private set; }
    public DateTime? ValidationDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
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
        UserValidation validation,
        DateTime createdAt) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        PasswordSalt = passwordSalt;
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
        string salt = passwordSalt ?? Hasher.GenerateSalt();
        string hashedPassword = Hasher.ComputeHash(
            plainPassword,
            salt,
            1000);

        var user = new User(
            id,
            firstName,
            lastName,
            email,
            hashedPassword,
            salt,
            userRole,
            UserValidation.NotValidated,
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

    public bool IsValidPassword(string plainPassword)
    {
        string hashedPassword = Hasher.ComputeHash(
            plainPassword,
            PasswordSalt,
            1000);

        return Password == hashedPassword;
    }

    public void Update(
       string firstName,
       string lastName,
       string email,
       string plainPassword,
       UserRole userRole,
       DateTime updatedAt)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserRole = userRole;
        UpdatedAt = updatedAt;

        string hashedPassword = Hasher.ComputeHash(
                plainPassword,
                PasswordSalt,
                1000);

        if (hashedPassword != Password)
        {
            PasswordSalt ??= Hasher.GenerateSalt();
            Password = Hasher.ComputeHash(
                plainPassword,
                PasswordSalt,
                1000);
        }

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
        Validation = UserValidation.Validated;
        ValidationDate = validationDate;
    }

#pragma warning disable CS8618
    private User()
    {
    }
#pragma warning restore CS8618
}
