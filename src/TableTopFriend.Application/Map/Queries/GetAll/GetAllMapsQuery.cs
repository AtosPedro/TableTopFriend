using ErrorOr;
using MediatR;
using TableTopFriend.Application.Map.Common;

namespace TableTopFriend.Application.Map.Queries.GetAll;

public record GetAllMapsQuery(
    Guid UserId
) : IRequest<ErrorOr<IReadOnlyList<MapResult>>>;
