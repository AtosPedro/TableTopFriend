using TableTopFriend.Application.Users.Common;
using TableTopFriend.Domain.Common.Enums;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Users.Commands.Delete;

public record UpdateUserCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    byte[] ProfileImage,
    string Password,
    UserRole Role
): IRequest<ErrorOr<UserResult>>;
