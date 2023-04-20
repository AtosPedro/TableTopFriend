using DDDTableTopFriend.Application.Sessions.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Sessions.Commands.Schedule;

public class ScheduleSessionCommandHandler : IRequestHandler<ScheduleSessionCommand, ErrorOr<SessionResult>>
{
    public Task<ErrorOr<SessionResult>> Handle(
        ScheduleSessionCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
