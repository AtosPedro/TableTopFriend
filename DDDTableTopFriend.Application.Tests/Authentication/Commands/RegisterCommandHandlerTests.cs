using DDDTableTopFriend.Application.Authentication.Commands.Register;
using DDDTableTopFriend.Application.Authentication.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Authentication;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.Common.Enums;
using NUnit.Framework;
using Moq;

namespace DDDTableTopFriend.Application.Tests.Authentication.Commands;

[TestFixture]
[Author("Atos Pedro")]
public class RegisterCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IJwtTokenGenerator> _jwtTokenGenerator;
    private readonly Mock<IDateTimeProvider> _dateTimeProvider;
    private readonly Mock<IUnitOfWork> _unitOfWork;

    public RegisterCommandHandlerTests()
    {
        _userRepositoryMock = new();
        _jwtTokenGenerator = new();
        _dateTimeProvider = new();
        _unitOfWork = new();
    }

    [Test]
    public async Task Handle_Should_Return_UserWithToken()
    {
        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "johndoe@email.com";
        const string password = "123456789";
        const UserRole role = UserRole.Administrator;
        var command = new RegisterCommand(firstName, lastName, email, password, role);
        var handler = new RegisterCommandHandler(
            _userRepositoryMock.Object,
            _jwtTokenGenerator.Object,
            _dateTimeProvider.Object,
            _unitOfWork.Object
        );

        _unitOfWork.Setup(
            x => x.Execute(
                It.IsAny<Func<CancellationToken,Task<AuthenticationResult>>>(),
                It.IsAny<CancellationToken>()
            )
        ).ReturnsAsync(new AuthenticationResult{
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI3NDgxOTU4Ny1iMWU5LTQxZjUtODk5Yy1mODEzYWE3Mjg3ZTgiLCJqdGkiOiJjMmFmODhiZC1mYTAzLTQwOGItYTQ2Zi0xMjA0OTI4MWM1NzgiLCJnaXZlbl9uYW1lIjoiSm9obiIsImZhbWlseV9uYW1lIjoiRG9lIiwiZXhwIjoxNjgzNDExODY0LCJpc3MiOiJUYWJsZVRvcCBGcmllbmQiLCJhdWQiOiJUYWJsZVRvcCBGcmllbmQifQ.MmyYuvaF_IyOyy0tCmf1QlBeTUNDqDz2azXQhddoh90"
        });

        var result = await handler.Handle(command, default);

        Assert.Multiple(() =>
        {
            Assert.That(result.Value.FirstName, Is.EqualTo(firstName));
            Assert.That(result.Value.LastName, Is.EqualTo(lastName));
            Assert.That(result.Value.Email, Is.EqualTo(email));
            Assert.That(result.Value.Id, Is.Not.EqualTo(default(Guid)));
            Assert.That(result.Value.Token, Is.Not.Null);
            Assert.That(result.Value.Token, Is.Not.Empty);
        });
    }
}
