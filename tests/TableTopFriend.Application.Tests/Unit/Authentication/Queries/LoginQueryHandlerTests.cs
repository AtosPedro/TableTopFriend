using TableTopFriend.Application.Authentication.Queries.Login;
using TableTopFriend.Application.Common.Interfaces.Authentication;
using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Domain.AggregateUser;
using TableTopFriend.Domain.AggregateUser.Enums;
using MediatR;
using Moq;
using NUnit.Framework;

namespace TableTopFriend.Application.Tests.Authentication.Queries;

[TestFixture]
public class LoginQueryHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;
    private readonly Mock<IPublisher> _publisherMock;

    public LoginQueryHandlerTests()
    {
        _userRepositoryMock = new();
        _jwtTokenGeneratorMock = new();
        _dateTimeProviderMock = new();
        _publisherMock = new();
    }

    [Test]

    public async Task Handle_Should_Return_UserWithToken()
    {
        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "johndoe@email.com";
        const string password = "123456789";
        const UserRole role = UserRole.Administrator;
        var command = new LoginQuery(email, password);
        var handler = new LoginQueryHandler(
            _userRepositoryMock.Object,
            _jwtTokenGeneratorMock.Object,
            _publisherMock.Object,
            _dateTimeProviderMock.Object
        );

        User mockUser = User.Create(
            firstName,
            lastName,
            email,
            password,
            null,
            role,
            _dateTimeProviderMock.Object.UtcNow
        ).Value;

        _userRepositoryMock.Setup(
            x => x.GetUserByEmail(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            )
        ).ReturnsAsync(mockUser);

        _jwtTokenGeneratorMock.Setup(
            x => x.GenerateToken(
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            )
        ).Returns("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI3NDgxOTU4Ny1iMWU5LTQxZjUtODk5Yy1mODEzYWE3Mjg3ZTgiLCJqdGkiOiJjMmFmODhiZC1mYTAzLTQwOGItYTQ2Zi0xMjA0OTI4MWM1NzgiLCJnaXZlbl9uYW1lIjoiSm9obiIsImZhbWlseV9uYW1lIjoiRG9lIiwiZXhwIjoxNjgzNDExODY0LCJpc3MiOiJUYWJsZVRvcCBGcmllbmQiLCJhdWQiOiJUYWJsZVRvcCBGcmllbmQifQ.MmyYuvaF_IyOyy0tCmf1QlBeTUNDqDz2azXQhddoh90");

        var result = await handler.Handle(command, default);

        Assert.Multiple(() =>
        {
            Assert.That(result.Value.Email, Is.EqualTo(email));
            Assert.That(result.Value.FirstName, Is.EqualTo(firstName));
            Assert.That(result.Value.LastName, Is.EqualTo(lastName));
            Assert.That(result.Value.Token, Is.Not.Null);
            Assert.That(result.Value.Token, Is.Not.Empty);
        });
    }
}
