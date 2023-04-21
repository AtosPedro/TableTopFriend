using DDDTableTopFriend.Application.Sessions.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Sessions.Commands.Update;

public record UpdateSessionCommand(
    Guid UserId,
    Guid CampaignId,
    string Name,
    DateTime DateTime,
    TimeSpan Duration
) : IRequest<ErrorOr<SessionResult>>;
