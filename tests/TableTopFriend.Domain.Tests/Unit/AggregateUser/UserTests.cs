using TableTopFriend.Domain.AggregateUser;
using TableTopFriend.Domain.AggregateUser.Events;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Enums;
using NUnit.Framework;

namespace TableTopFriend.Domain.Tests.AggregateUser;

[TestFixture]
[Author("Atos Pedro")]
public class UserTests
{
    [Test]
    [Author("Atos Pedro")]
    public void Create_User_Should_Return_Valid_User()
    {
        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "johndoe@email.com";
        const string password = "12345678";
        const string passwordSalt = "3KUbX/owfJ+OP9NZB303RQ==";
        const string passwordHash = "gPtaIgXCaqRuNa2Nd5QuaIw2JP54GONRZcZIfKTQOX0=";
        const UserRole role = UserRole.Administrator;
        Email emailVo = Email.Create(email).Value;
        var user = User.Create(
            firstName,
            lastName,
            email,
            password,
            passwordSalt,
            role,
            DateTime.UtcNow
        ).Value;

        Assert.Multiple(() =>
        {
            Assert.That(user.FirstName, Is.EqualTo(firstName));
            Assert.That(user.LastName, Is.EqualTo(lastName));
            Assert.That(user.Email, Is.EqualTo(emailVo));
            Assert.That(user.Password, Is.EqualTo(Password.Create(passwordHash, passwordSalt)));
        });
    }

    [Test]
    [Author("Atos Pedro")]
    public void Update_User_Should_Return_Valid_User()
    {
        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "johndoe@email.com";
        const string password = "12345678";
        const string passwordSalt = "3KUbX/owfJ+OP9NZB303RQ==";
        const UserRole role = UserRole.Administrator;

        const string firstNameUpdated = "John Updated";
        const string lastNameUpdated = "Doe Updated";
        const string emailUpdated = "johndoe@emailUpdated.com";
        const UserRole roleUpdated = UserRole.FreeUser;

        Email emailUpdatedVo = Email.Create(emailUpdated).Value;

        var user = User.Create(
            firstName,
            lastName,
            email,
            password,
            passwordSalt,
            role,
            DateTime.UtcNow
        ).Value;

        user.Update(
            firstNameUpdated,
            lastNameUpdated,
            emailUpdated,
            roleUpdated,
            DateTime.UtcNow
        );

        Assert.Multiple(() =>
        {
            Assert.That(user.FirstName, Is.EqualTo(firstNameUpdated));
            Assert.That(user.LastName, Is.EqualTo(lastNameUpdated));
            Assert.That(user.Email, Is.EqualTo(emailUpdatedVo));
            Assert.That(user.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    [Author("Atos Pedro")]
    public void Mark_To_Delete_Should_Return_Deleted_User_Domain_Event_Valid()
    {
        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "johndoe@email.com";
        const string password = "12345678";
        const string passwordSalt = "3KUbX/owfJ+OP9NZB303RQ==";
        const UserRole role = UserRole.Administrator;

        var user = User.Create(
            firstName,
            lastName,
            email,
            password,
            passwordSalt,
            role,
            DateTime.UtcNow
        ).Value;

        user.ClearDomainEvents();
        var deletedAt = DateTime.UtcNow;
        user.MarkToDelete(deletedAt);
        var deleteUserDomainEvent = user.GetDomainEvents().FirstOrDefault() as DeletedUserDomainEvent;

        Assert.Multiple(() =>
        {
            Assert.That(deleteUserDomainEvent!.DeletedDate, Is.EqualTo(deletedAt));
            Assert.That(deleteUserDomainEvent!.userId, Is.EqualTo(UserId.Create(user.Id.Value)));
            Assert.That(deleteUserDomainEvent!.userId, Is.Not.Null);
        });
    }

    [Test]
    [Author("Atos Pedro")]
    public void Is_Valid_Password_Should_Return_True_When_Password_Is_Correct()
    {
        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "johndoe@email.com";
        const string password = "12345678";
        const string passwordSalt = "3KUbX/owfJ+OP9NZB303RQ==";
        const UserRole role = UserRole.Administrator;

        var user = User.Create(
            firstName,
            lastName,
            email,
            password,
            passwordSalt,
            role,
            DateTime.UtcNow
        ).Value;

        Assert.That(user.Password.IsValid(password), Is.True);
    }

    [Test]
    [Author("Atos Pedro")]
    public void Is_Valid_Password_Should_Return_False_When_Password_Is_Incorrect()
    {
        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "johndoe@email.com";
        const string password = "12345678";
        const string passwordSalt = "3KUbX/owfJ+OP9NZB303RQ==";
        const UserRole role = UserRole.Administrator;

        var user = User.Create(
            firstName,
            lastName,
            email,
            password,
            passwordSalt,
            role,
            DateTime.UtcNow
        ).Value;

        Assert.That(user.Password.IsValid("abcdefghijkl"), Is.False);
    }

    [Test]
    [Author("Atos Pedro")]
    public void Validate_Should_Update_Validation_And_Validation_Date()
    {
        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "johndoe@email.com";
        const string password = "12345678";
        const string passwordSalt = "3KUbX/owfJ+OP9NZB303RQ==";
        const UserRole role = UserRole.Administrator;

        var user = User.Create(
            firstName,
            lastName,
            email,
            password,
            passwordSalt,
            role,
            DateTime.UtcNow
        ).Value;
        var validationDate = DateTime.UtcNow;
        user.Validation.Validate(validationDate);

        Assert.Multiple(() =>
        {
            Assert.That(user.Validation.Value, Is.EqualTo(StatusValidation.Validated));
            Assert.That(user.Validation.ValidationDate, Is.EqualTo(validationDate));
        });
    }
}
