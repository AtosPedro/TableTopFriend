using DDDTableTopFriend.Application.Authentication.Common;
using DDDTableTopFriend.Domain.Common.Enums;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    UserRole Role
) : IRequest<ErrorOr<AuthenticationResult>>;
