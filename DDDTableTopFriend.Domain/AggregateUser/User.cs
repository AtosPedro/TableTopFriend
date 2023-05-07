using DDDTableTopFriend.Domain.Common.Enums;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.Events;
using ErrorOr;

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

    public static ErrorOr<User> Create(
        string firstName,
        string lastName,
        string email,
        string plainPassword,
        string? passwordSalt,
        UserRole userRole,
        DateTime createdAt)
    {
        var errors = new List<Error>();
        var id = UserId.CreateUnique();
        var emailVo = Email.Create(email);
        var password = Password.CreateHashed(plainPassword, passwordSalt);

        if (emailVo.IsError)
            return emailVo.Errors;

        var validation = Validation.Create();
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
    
    public ErrorOr<User> ChangePassword(string plainPassword)
    {
        var passwordOrError = Password.CreateHashed(plainPassword, Password.Salt);
        if (passwordOrError.IsError)
            return passwordOrError.Errors;

        Password = passwordOrError.Value;
        return this;
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
