using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateUser.Events;
using DDDTableTopFriend.Domain.Common.ValueObjects;
using MediatR;

namespace DDDTableTopFriend.Application.Users.EventHandlers;

public class SendConfirmationMail : INotificationHandler<UserRegisteredDomainEvent>
{
    private readonly IMailService _mailService;
    public SendConfirmationMail(IMailService mailService)
    {
        _mailService = mailService;
    }

    public async Task Handle(
        UserRegisteredDomainEvent notification,
        CancellationToken cancellationToken)
    {
        string link = $"v1/api/users/validate/{notification.Id.Value}";
        var mail = Mail.Create(
            notification.Email,
            "Table Top Friend User Confirmation Email",
            $"Welcome to Table Top Friend, here's your link to validate your account: {link}"
        );

        await _mailService.SendMail(
            mail.To,
            mail.Subject,
            mail.Body
        );
    }
}
