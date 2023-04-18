using DDDTableTopFriend.Application.Statuses.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Statuses.Commands.Update;

public record UpdateStatusCommand(
    Guid StatusId,
    string Name,
    string Description,
    float Quantity
) : IRequest<ErrorOr<StatusResult>>;
