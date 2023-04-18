using DDDTableTopFriend.Application.Users.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Users.Queries.Get;

public record GetUserQuery(
    Guid UserId
) : IRequest<ErrorOr<UserResult>>;
