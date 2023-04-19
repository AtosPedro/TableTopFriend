using DDDTableTopFriend.Application.Campaigns.Common;
using DDDTableTopFriend.Application.Campaigns.GetAll.Queries;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.GetAll;

public class GetAllCampaignQueryHandler : IRequestHandler<GetAllCampaignQuery, ErrorOr<IEnumerable<CampaignResult>>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ICachingService _cachingService;
    public GetAllCampaignQueryHandler(
        ICampaignRepository campaignRepository,
        ICachingService cachingService)
    {
        _campaignRepository = campaignRepository;
        _cachingService = cachingService;
    }

    public async Task<ErrorOr<IEnumerable<CampaignResult>>> Handle(
        GetAllCampaignQuery request,
        CancellationToken cancellationToken)
    {
        var cachedCampaigns = await _cachingService.GetManyCacheValueAsync<CampaignResult>(w => w.UserId == request.UserId);
        if (cachedCampaigns.Any())
            return cachedCampaigns;

        var campaigns = await _campaignRepository.GetAll(
            UserId.Create(request.UserId),
            cancellationToken);

        var campaignsResult = new List<CampaignResult>();
        foreach (var campaign in campaigns)
        {
            var result = campaign.Adapt<CampaignResult>();
            await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
            campaignsResult.Add(result);
        }

        return campaignsResult;
    }
}
