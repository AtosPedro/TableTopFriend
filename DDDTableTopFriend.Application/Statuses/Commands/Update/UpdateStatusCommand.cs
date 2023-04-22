using DDDTableTopFriend.Application.Statuses.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Statuses.Commands.Update;

public record UpdateStatusCommand(
    Guid Id,
    string Name,
    string Description,
    float Quantity
) : IRequest<ErrorOr<StatusResult>>;
