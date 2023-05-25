using TableTopFriend.Application.Sessions.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Sessions.Commands.Schedule;

public record ScheduleSessionCommand(
    Guid UserId,
    Guid CampaignId,
    string Name,
    string Description,
    DateTime DateTime
) : IRequest<ErrorOr<SessionResult>>;
