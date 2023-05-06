using DDDTableTopFriend.Application.Sessions.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Sessions.Commands.Schedule;

public record ScheduleSessionCommand(
    Guid UserId,
    Guid CampaignId,
    string Name,
    string Description,
    DateTime DateTime
) : IRequest<ErrorOr<SessionResult>>;
