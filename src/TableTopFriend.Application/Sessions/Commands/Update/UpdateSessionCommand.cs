using TableTopFriend.Application.Sessions.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Sessions.Commands.Update;

public record UpdateSessionCommand(
    Guid Id,
    string Name,
    string Description,
    DateTime DateTime,
    TimeSpan Duration
) : IRequest<ErrorOr<SessionResult>>;
