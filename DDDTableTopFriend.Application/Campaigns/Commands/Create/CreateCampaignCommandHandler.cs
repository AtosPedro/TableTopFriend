using DDDTableTopFriend.Application.Campaigns.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateCampaign;
using DDDTableTopFriend.Domain.AggregateCampaign.Events;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Create;

public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, ErrorOr<CampaignResult>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateCampaignCommandHandler(
        ICampaignRepository campaignRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _campaignRepository = campaignRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<CampaignResult>> Handle(
        CreateCampaignCommand request,
        CancellationToken cancellationToken)
    {
        var campaign = await _campaignRepository.GetByName(
            request.Name,
            UserId.Create(request.UserId),
            cancellationToken);

        if (campaign is not null)
            return Domain.Common.Errors.Errors.Campaign.DuplicateName;

        campaign = Campaign.Create(
            UserId.Create(request.UserId),
            request.Name,
            request.Description,
            request.CharacterIds.ConvertAll(x => CharacterId.Create(x)),
            _dateTimeProvider.UtcNow
        );

        await _campaignRepository.Add(campaign, cancellationToken);
        return campaign.Adapt<CampaignResult>();
    }
}
