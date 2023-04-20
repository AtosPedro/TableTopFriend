using DDDTableTopFriend.Application.Sessions.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Sessions.Commands.Update;

public class UpdateSessionCommandHandler : IRequestHandler<UpdateSessionCommand, ErrorOr<SessionResult>>
{
    public Task<ErrorOr<SessionResult>> Handle(
        UpdateSessionCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
