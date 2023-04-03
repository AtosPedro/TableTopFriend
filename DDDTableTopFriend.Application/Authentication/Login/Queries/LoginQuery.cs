using DDDTableTopFriend.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Authentication.Login.Queries;

public record LoginQuery(
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;
