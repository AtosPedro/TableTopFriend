using DDDTableTopFriend.Application.Campaigns.Common;
using DDDTableTopFriend.Application.Campaigns.GetAll.Queries;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.GetAll;

public class GetAllCampaignQueryHandler : IRequestHandler<GetAllCampaignQuery, ErrorOr<IEnumerable<CampaignResult>>>
{
    private readonly ICampaignRepository _campaignRepository;
    public GetAllCampaignQueryHandler(ICampaignRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;
    }

    public async Task<ErrorOr<IEnumerable<CampaignResult>>> Handle(
        GetAllCampaignQuery request,
        CancellationToken cancellationToken)
    {
        var campaigns = await _campaignRepository.GetAll(
            UserId.Create(request.UserId),
            cancellationToken);

        var campaignsResult = new List<CampaignResult>();
        foreach (var campaign in campaigns)
            campaignsResult.Add(campaign.Adapt<CampaignResult>());

        return campaignsResult;
    }
}
