using DDDTableTopFriend.Application.Users.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Users.Commands.Validate;

public record ValidateUserCommand(
    Guid UserId
) : IRequest<ErrorOr<UserResult>>;
