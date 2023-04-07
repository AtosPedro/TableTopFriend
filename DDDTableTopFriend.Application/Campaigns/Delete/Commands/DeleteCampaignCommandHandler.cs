using DDDTableTopFriend.Application.Campaigns.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Delete.Commands;

public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand, ErrorOr<CampaignResult>>
{
    private readonly ICampaignRepository _campaignRepository;
    public DeleteCampaignCommandHandler(ICampaignRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;
    }

    public async Task<ErrorOr<CampaignResult>> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
    {
        var campaign = _campaignRepository.GetById(request.Id);
        if(campaign is null)
            return Errors.Campaign.NotRegistered;

        _campaignRepository.Remove(request.Id);

        return request.Adapt<CampaignResult>();
    }
}
