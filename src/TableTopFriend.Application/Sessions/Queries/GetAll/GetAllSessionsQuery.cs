using TableTopFriend.Application.Sessions.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Sessions.Queries.GetAll;

public record GetAllSessionsQuery(
    Guid UserId,
    Guid CampaignId
) : IRequest<ErrorOr<IReadOnlyList<SessionResult>>>;
