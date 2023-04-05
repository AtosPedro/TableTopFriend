using DDDTableTopFriend.Application.Campaigns.Common;
using DDDTableTopFriend.Domain.Common.Errors;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;
using Mapster;

namespace DDDTableTopFriend.Application.Campaigns.Get.Queries;

public class GetCampaignQueryHandler : IRequestHandler<GetCampaignQuery, ErrorOr<CampaignResult>>
{
    private readonly ICampaignRepository _campaignRepository;
    public GetCampaignQueryHandler(ICampaignRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;
    }

    public async Task<ErrorOr<CampaignResult>> Handle(
        GetCampaignQuery request,
        CancellationToken cancellationToken)
    {
        var campaign = _campaignRepository.GetById(request.id);
        if(campaign is null)
            return Errors.Campaign.NotRegistered;

        return campaign.Adapt<CampaignResult>();
    }
}
