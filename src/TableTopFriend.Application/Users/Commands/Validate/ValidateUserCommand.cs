using TableTopFriend.Application.Users.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Users.Commands.Validate;

public record ValidateUserCommand(
    Guid UserId
) : IRequest<ErrorOr<UserResult>>;
