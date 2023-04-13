using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateUser.Events;
using MediatR;

namespace DDDTableTopFriend.Application.Authentication.EventHandlers.Register;

public class UserRegisteredDomainEventHandler : INotificationHandler<UserRegisteredDomainEvent>
{
    private readonly IMailService _mailService;
    public UserRegisteredDomainEventHandler(IMailService mailService)
    {
        _mailService = mailService;
    }
    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);
    }
}
