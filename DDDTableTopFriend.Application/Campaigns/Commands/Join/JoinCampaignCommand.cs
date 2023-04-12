using DDDTableTopFriend.Application.Campaigns.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Join;

public record JoinCampaignCommand(
    Guid Id,
    Guid CharacterId
) : IRequest<ErrorOr<CampaignJoinedResult>>;
