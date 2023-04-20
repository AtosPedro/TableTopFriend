using DDDTableTopFriend.Application.Sessions.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Sessions.Commands.Schedule;

public record ScheduleSessionCommand(

) : IRequest<ErrorOr<SessionResult>>;
