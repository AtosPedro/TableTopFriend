using DDDTableTopFriend.Application.Sessions.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Sessions.Queries.Get;

public class GetSessionQueryHandler : IRequestHandler<GetSessionQuery, ErrorOr<SessionResult>>
{
    public Task<ErrorOr<SessionResult>> Handle(
        GetSessionQuery request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
