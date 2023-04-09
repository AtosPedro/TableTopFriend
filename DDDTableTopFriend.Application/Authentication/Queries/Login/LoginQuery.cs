using DDDTableTopFriend.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;
