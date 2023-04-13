using DDDTableTopFriend.Domain.AggregateUser.Events;
using MediatR;

namespace DDDTableTopFriend.Application.Authentication.EventHandlers.Login;

public class UserAuthenticatedDomainEventHandler : INotificationHandler<UserAuthenticatedDomainEvent>
{
    public async Task Handle(UserAuthenticatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);
    }
}
