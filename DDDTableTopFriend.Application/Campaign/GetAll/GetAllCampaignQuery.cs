using DDDTableTopFriend.Application.Campaigns.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.GetAll.Queries;

public record GetAllCampaignQuery(
    Guid userId
) : IRequest<ErrorOr<IEnumerable<CampaignResult>>>;
