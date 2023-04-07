using DDDTableTopFriend.Application.Campaigns.Common;
using DDDTableTopFriend.Application.Campaigns.Create.Commands;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.Campaign;
using DDDTableTopFriend.Domain.Character.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using DDDTableTopFriend.Domain.Session.ValueObjects;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Update.Commands;

public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, ErrorOr<CampaignResult>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    public UpdateCampaignCommandHandler(
        ICampaignRepository campaignRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _campaignRepository = campaignRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<CampaignResult>> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
    {
        var campaign = _campaignRepository.GetById(request.Id);
        if(campaign is null)
            return Errors.Campaign.NotRegistered;

        campaign = request.Adapt<Campaign>();
        campaign = Campaign.Update(campaign, _dateTimeProvider.UtcNow);
        _campaignRepository.Update(campaign);
        return campaign.Adapt<CampaignResult>();
    }
}
