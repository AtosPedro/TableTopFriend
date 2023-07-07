using ErrorOr;
using MediatR;
using TableTopFriend.Application.Map.Common;

namespace TableTopFriend.Application.Map.Queries.Get;

public record GetMapQuery(
    Guid Id
) : IRequest<ErrorOr<MapResult>>;
