using DDDTableTopFriend.Domain.AggregateUser.Events;
using MediatR;

namespace DDDTableTopFriend.Application.Users.EventHandlers;

public class SendMailUserDeletedDomainEventHandler : INotificationHandler<DeletedUserDomainEvent>
{
    public Task Handle(DeletedUserDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
