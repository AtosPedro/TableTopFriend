using DDDTableTopFriend.Application.Statuses.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Statuses.Queries.Get;

public record GetStatusQuery(
    Guid Id
) : IRequest<ErrorOr<StatusResult>>;
