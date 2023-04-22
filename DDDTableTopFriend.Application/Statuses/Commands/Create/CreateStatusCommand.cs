using DDDTableTopFriend.Application.Statuses.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Statuses.Commands.Create;

public record CreateStatusCommand(
    Guid UserId,
    string Name,
    string Description,
    float Quantity
) : IRequest<ErrorOr<StatusResult>>;
