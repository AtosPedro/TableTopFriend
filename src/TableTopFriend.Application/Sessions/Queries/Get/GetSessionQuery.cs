using TableTopFriend.Application.Sessions.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Sessions.Queries.Get;

public record GetSessionQuery(
    Guid Id
) : IRequest<ErrorOr<SessionResult>>;
