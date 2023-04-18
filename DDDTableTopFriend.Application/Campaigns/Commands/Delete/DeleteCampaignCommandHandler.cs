using DDDTableTopFriend.Application.Campaigns.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Delete;

public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand, ErrorOr<CampaignResult>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    public DeleteCampaignCommandHandler(
        ICampaignRepository campaignRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _campaignRepository = campaignRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<CampaignResult>> Handle(
        DeleteCampaignCommand request,
        CancellationToken cancellationToken)
    {
        var campaign = await _campaignRepository.GetById(
            CampaignId.Create(request.Id),
            cancellationToken);

        if(campaign is null)
            return Errors.Campaign.NotRegistered;

        campaign.MarkToDelete(_dateTimeProvider.UtcNow);
        await _campaignRepository.Remove(campaign);
        return request.Adapt<CampaignResult>();
    }
}
