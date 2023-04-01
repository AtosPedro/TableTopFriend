using DDDTableTopFriend.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Authentication.Register.Commands;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;
