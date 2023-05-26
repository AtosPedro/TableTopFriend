using TableTopFriend.Application.Authentication.EventHandlers;
using TableTopFriend.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace TableTopFriend.Application.Tests.Authentication.EventHandlers;

[TestFixture]
public class SendConfirmationEmailTests
{
    private readonly Mock<IMailService> _mailServiceMock;
    private readonly Mock<ILogger<SendConfirmationMail>> _loggerMock;
    public SendConfirmationEmailTests()
    {
        _mailServiceMock = new();
        _loggerMock = new();
    }

    public void Handle_ShouldNot_ThrowException()
    {
        _mailServiceMock.Setup(
            x => x.SendConfirmationMail(
                It.IsAny<string>(),
                It.IsAny<string>()
            )
        ).ReturnsAsync("success");

        var eventHandler = new SendConfirmationMail(
            _mailServiceMock.Object,
            _loggerMock.Object
        );
    }
}
