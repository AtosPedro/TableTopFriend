using DDDTableTopFriend.Application.Users.Common;
using DDDTableTopFriend.Domain.Common.Enums;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Users.Commands.Delete;

public record UpdateUserCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    byte[] ProfileImage,
    string Password,
    UserRole Role
): IRequest<ErrorOr<UserResult>>;
