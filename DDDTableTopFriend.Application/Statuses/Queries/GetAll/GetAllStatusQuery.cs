using DDDTableTopFriend.Application.Statuses.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Statuses.Queries.GetAll;

public record GetAllStatusQuery(
    Guid UserId
) : IRequest<ErrorOr<IReadOnlyList<StatusResult>>>;
