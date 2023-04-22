using DDDTableTopFriend.Application.Sessions.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Sessions.Commands.Update;

public record UpdateSessionCommand(
    Guid Id,
    string Name,
    DateTime DateTime,
    TimeSpan Duration
) : IRequest<ErrorOr<SessionResult>>;
