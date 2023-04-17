using DDDTableTopFriend.Application.Campaigns.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Get.Queries;

public record GetCampaignQuery(
    Guid Id
) : IRequest<ErrorOr<CampaignResult>>;
