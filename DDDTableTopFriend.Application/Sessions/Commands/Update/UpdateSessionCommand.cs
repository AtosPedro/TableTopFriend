using DDDTableTopFriend.Application.Sessions.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Sessions.Commands.Update;

public record UpdateSessionCommand(

) : IRequest<ErrorOr<SessionResult>>;
