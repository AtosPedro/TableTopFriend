using TableTopFriend.Domain.Common.Enums;
using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.AggregateUser.Events;
using ErrorOr;

namespace TableTopFriend.Domain.AggregateUser;

public sealed class User : AggregateRoot<UserId, Guid>
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Password Password { get; set; } = null!;
    public byte[]? ProfileImage { get; private set; }
    public UserRole UserRole { get; private set; }
    public Validation Validation { get; private set; } = null!;
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

    public static ErrorOr<User> Create(
        string firstName,
        string lastName,
        string email,
        string plainPassword,
        string? passwordSalt,
        UserRole userRole,
        DateTime createdAt)
    {
        UserId id = UserId.CreateUnique();
        var emailVo = Email.Create(email);
        var password = Password.CreateHashed(plainPassword, passwordSalt);
        var validation = Validation.Create();

        var errors = HandleErrors(
            emailVo,
            password
        );

        if(errors.Any())
            return errors;

        var user = new User(
            id,
            firstName,
            lastName,
            emailVo.Value,
            password.Value,
            userRole,
            validation,
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

    public ErrorOr<User> Update(
       string firstName,
       string lastName,
       string email,
       UserRole userRole,
       DateTime updatedAt)
    {
        var emailVo = Email.Create(email);

        if (emailVo.IsError)
            return emailVo.Errors;

        FirstName = firstName;
        LastName = lastName;
        Email = emailVo.Value;
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

        return this;
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
