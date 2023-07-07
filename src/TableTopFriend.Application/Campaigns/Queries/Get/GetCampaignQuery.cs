using TableTopFriend.Application.Campaigns.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Campaigns.Get.Queries;

public record GetCampaignQuery(
    Guid Id
) : IRequest<ErrorOr<CampaignResult>>;
