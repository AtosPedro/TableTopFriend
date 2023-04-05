using DDDTableTopFriend.Application.Campaigns.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.Campaign;
using DDDTableTopFriend.Domain.Character.ValueObjects;
using DDDTableTopFriend.Domain.Session.ValueObjects;
using DDDTableTopFriend.Domain.Users.ValueObjects;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Create.Commands;

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

    public async Task<ErrorOr<CampaignResult>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
    {
        var campaign = _campaignRepository.GetByName(request.Name);
        if (campaign is not null)
            return Domain.Common.Errors.Errors.Campaign.DuplicateName;

        var cam = request.Adapt<Campaign>();
        campaign = Campaign.Create(
            UserId.Create(request.UserId),
            request.Name,
            request.Description,
            cam.CharacterIds.ToList(),
            cam.SessionIds.ToList(),
            _dateTimeProvider.UtcNow,
            null
        );

        _campaignRepository.Add(campaign);

        return campaign.Adapt<CampaignResult>();
    }
}
