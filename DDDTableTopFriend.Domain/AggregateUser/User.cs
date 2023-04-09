﻿using DDDTableTopFriend.Domain.Common.Enums;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateUser;

public sealed class User : AggregateRoot<UserId>
{
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string Password { get; }
    public string PasswordSalt { get; }
    public UserRole UserRole { get; }
    public DateTime? CreatedAt { get; }
    public DateTime? UpdatedAt { get; }

    public User(
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
        DateTime? createdAt = null,
        DateTime? updatedAt = null)
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
            updatedAt);
    }

    public static User Update(
       UserId id,
       string firstName,
       string lastName,
       string email,
       string password,
       string passwordSalt,
       UserRole userRole,
       DateTime? createdAt = null,
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
}
