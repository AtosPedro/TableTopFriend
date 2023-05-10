using DDDTableTopFriend.Application.Common.Interfaces.Services;
using Moq;

namespace DDDTableTopFriend.Application.Tests.Authentication.EventHandlers;

public class SendConfirmationEmailTests
{
    private readonly Mock<IMailService> _mailServiceMock;
    public SendConfirmationEmailTests()
    {
        _mailServiceMock = new();
    }
}
