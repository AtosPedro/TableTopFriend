using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Users.Commands.Delete;

public record DeleteUserCommand(
    Guid Id
): IRequest<ErrorOr<bool>>;
