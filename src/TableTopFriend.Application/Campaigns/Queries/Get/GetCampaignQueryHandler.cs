using TableTopFriend.Application.Campaigns.Common;
using TableTopFriend.Domain.Common.Errors;
using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Domain.AggregateCampaign.ValueObjects;
using TableTopFriend.Application.Common.Interfaces.Services;
using ErrorOr;
using MediatR;
using Mapster;

namespace TableTopFriend.Application.Campaigns.Get.Queries;

public class GetCampaignQueryHandler : IRequestHandler<GetCampaignQuery, ErrorOr<CampaignResult>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ICachingService _cachingService;
    public GetCampaignQueryHandler(ICampaignRepository campaignRepository, ICachingService cachingService)
    {
        _campaignRepository = campaignRepository;
        _cachingService = cachingService;
    }

    public async Task<ErrorOr<CampaignResult>> Handle(
        GetCampaignQuery request,
        CancellationToken cancellationToken)
    {
        var cachedCampaign = await _cachingService.GetCacheValueAsync<CampaignResult>(request.Id.ToString());
        if(cachedCampaign is not null)
            return cachedCampaign;

        var campaign = await _campaignRepository.GetById(
            CampaignId.Create(request.Id),
            cancellationToken);

        if(campaign is null)
            return Errors.Campaign.NotRegistered;

        var result = campaign.Adapt<CampaignResult>();
        await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
        return result;
    }
}
