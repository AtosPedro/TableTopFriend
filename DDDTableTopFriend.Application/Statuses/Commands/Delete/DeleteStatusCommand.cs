using DDDTableTopFriend.Application.Statuses.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Statuses.Commands.Delete;

public record DeleteStatusCommand(
    Guid StatusId
) : IRequest<ErrorOr<bool>>;
