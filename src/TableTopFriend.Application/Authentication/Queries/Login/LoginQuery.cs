using TableTopFriend.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;
