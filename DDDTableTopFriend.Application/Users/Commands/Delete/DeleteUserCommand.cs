using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Users.Commands.Delete;

public record DeleteUserCommand(
    Guid Id
): IRequest<ErrorOr<bool>>;
