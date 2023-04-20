using DDDTableTopFriend.Application.Sessions.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Sessions.Queries.GetAll;

public class GetAllSessionsQueryHandler : IRequestHandler<GetAllSessionsQuery, ErrorOr<IReadOnlyList<SessionResult>>>
{
    public Task<ErrorOr<IReadOnlyList<SessionResult>>> Handle(
        GetAllSessionsQuery request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
