using TableTopFriend.Application.Campaigns.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Campaigns.GetAll.Queries;

public record GetAllCampaignQuery(
    Guid UserId
) : IRequest<ErrorOr<IEnumerable<CampaignResult>>>;
