using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateUser.Events;
using Microsoft.Extensions.Logging;
using System.Web;
using MediatR;

namespace DDDTableTopFriend.Application.Authentication.EventHandlers;

public class SendConfirmationMail : INotificationHandler<UserRegisteredDomainEvent>
{
    private readonly IMailService _mailService;
    private readonly ILogger<SendConfirmationMail> _logger;
    public SendConfirmationMail(
        IMailService mailService,
        ILogger<SendConfirmationMail> logger)
    {
        _mailService = mailService;
        _logger = logger;
    }

    public async Task Handle(
        UserRegisteredDomainEvent notification,
        CancellationToken cancellationToken)
    {
        try
        {
            string link = $"https://localhost:7044/v1/api/users/validate/{notification.Id.Value}";
            await _mailService.SendConfirmationMail(
                notification.Email,
                link
            );

            _logger.LogInformation(
                "Confirmation email sent to {@To} at utc {@Utc}",
                notification.Email,
                DateTime.UtcNow
            );
        }
        catch (Exception ex)
        {
            _logger.LogCritical(
                "A critical error has occurred: {@Error}",
                ex.Message
            );
        }
    }
}
