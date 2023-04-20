using DDDTableTopFriend.Application.Sessions.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Sessions.Queries.Get;

public record GetSessionQuery(
    Guid Id
) : IRequest<ErrorOr<SessionResult>>;
