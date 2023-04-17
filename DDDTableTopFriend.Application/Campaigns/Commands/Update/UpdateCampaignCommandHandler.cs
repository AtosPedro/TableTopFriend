using DDDTableTopFriend.Application.Campaigns.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateCampaign;
using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Update;

public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, ErrorOr<CampaignResult>>
{
    private readonly ICampaignRepository _campaignRepository;
    public UpdateCampaignCommandHandler(ICampaignRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;
    }

    public async Task<ErrorOr<CampaignResult>> Handle(
        UpdateCampaignCommand request,
        CancellationToken cancellationToken)
    {
        var campaign = await _campaignRepository.GetById(
            CampaignId.Create(request.Id),
            cancellationToken);

        if(campaign is null)
            return Errors.Campaign.NotRegistered;
        var characterList = request.CharacterIds.Adapt<List<CharacterId>>();
        var sessionList = request.SessionIds.Adapt<List<SessionId>>();
        campaign.Update(
            request.Name,
            request.Description,
            characterList,
            sessionList
        );

        await _campaignRepository.Update(campaign);
        return campaign.Adapt<CampaignResult>();
    }
}
