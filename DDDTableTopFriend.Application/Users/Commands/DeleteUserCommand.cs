using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Users.Commands;

public record DeleteUserCommand(
    Guid Id
): IRequest<ErrorOr<bool>>;
