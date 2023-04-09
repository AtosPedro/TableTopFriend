using DDDTableTopFriend.Application.Campaigns.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.GetAll.Queries;

public record GetAllCampaignQuery(
    Guid UserId
) : IRequest<ErrorOr<IEnumerable<CampaignResult>>>;
