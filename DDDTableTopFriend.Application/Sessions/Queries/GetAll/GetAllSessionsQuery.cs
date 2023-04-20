using DDDTableTopFriend.Application.Sessions.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Sessions.Queries.GetAll;

public record GetAllSessionsQuery(
    Guid UserId
) : IRequest<ErrorOr<IReadOnlyList<SessionResult>>>;
