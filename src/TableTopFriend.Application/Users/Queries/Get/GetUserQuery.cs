using TableTopFriend.Application.Users.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Users.Queries.Get;

public record GetUserQuery(
    Guid UserId
) : IRequest<ErrorOr<UserResult>>;
