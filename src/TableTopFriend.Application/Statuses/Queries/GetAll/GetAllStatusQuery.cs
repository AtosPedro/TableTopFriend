using TableTopFriend.Application.Statuses.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Statuses.Queries.GetAll;

public record GetAllStatusQuery(
    Guid UserId
) : IRequest<ErrorOr<IReadOnlyList<StatusResult>>>;
