using DDDTableTopFriend.Application.Campaigns.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Join;

public record JoinCampaignCommand(
    Guid id,
    Guid characterId
) : IRequest<ErrorOr<CampaignJoinedResult>>;
