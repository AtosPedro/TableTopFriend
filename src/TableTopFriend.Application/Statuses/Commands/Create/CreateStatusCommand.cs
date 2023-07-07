using TableTopFriend.Application.Statuses.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Statuses.Commands.Create;

public record CreateStatusCommand(
    Guid UserId,
    string Name,
    string Description,
    float Quantity
) : IRequest<ErrorOr<StatusResult>>;
