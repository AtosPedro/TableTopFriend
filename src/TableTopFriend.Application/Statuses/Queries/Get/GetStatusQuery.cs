using TableTopFriend.Application.Statuses.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Statuses.Queries.Get;

public record GetStatusQuery(
    Guid Id
) : IRequest<ErrorOr<StatusResult>>;
