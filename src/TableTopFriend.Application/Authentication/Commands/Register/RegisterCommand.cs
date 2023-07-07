using TableTopFriend.Application.Authentication.Common;
using TableTopFriend.Domain.Common.Enums;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    UserRole Role
) : IRequest<ErrorOr<AuthenticationResult>>;
