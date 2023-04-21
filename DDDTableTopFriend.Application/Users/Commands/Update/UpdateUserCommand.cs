using DDDTableTopFriend.Application.Users.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Users.Commands.Delete;

public record UpdateUserCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Password
): IRequest<ErrorOr<UserResult>>;
