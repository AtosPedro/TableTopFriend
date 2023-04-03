using DDDTableTopFriend.Application.Campaigns.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Domain.Entities;
using ErrorOr;
using Mapster;
using MediatR;
using DDDTableTopFriend.Domain.Common.Errors;
namespace DDDTableTopFriend.Application.Campaigns.Create.Commands;

public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, ErrorOr<CampaignResult>>
{
    private readonly ICampaignRepository _campaignRepository;
    public CreateCampaignCommandHandler(ICampaignRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;
    }

    public async Task<ErrorOr<CampaignResult>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
    {
        var campaign = request.Adapt<Campaign>();
        var dbCampaign = _campaignRepository.GetByName(campaign.Name);

        if(dbCampaign is not null)
            return Errors.Campaign.DuplicateName;

        _campaignRepository.Add(campaign);

        return campaign.Adapt<CampaignResult>();
    }
}
