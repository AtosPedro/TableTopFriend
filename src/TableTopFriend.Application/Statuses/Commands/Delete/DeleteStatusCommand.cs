using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Statuses.Commands.Delete;

public record DeleteStatusCommand(
    Guid StatusId
) : IRequest<ErrorOr<bool>>;
