using TableTopFriend.Application.Statuses.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Statuses.Commands.Update;

public record UpdateStatusCommand(
    Guid Id,
    string Name,
    string Description,
    float Quantity
) : IRequest<ErrorOr<StatusResult>>;
