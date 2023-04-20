using DDDTableTopFriend.Application.Sessions.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Sessions.Queries.GetAll;

public class GetAllSessionQueryHandler : IRequestHandler<GetAllSessionQuery, ErrorOr<IReadOnlyList<SessionResult>>>
{
    public Task<ErrorOr<IReadOnlyList<SessionResult>>> Handle(
        GetAllSessionQuery request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
