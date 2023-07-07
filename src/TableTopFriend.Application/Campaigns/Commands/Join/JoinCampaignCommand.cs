using TableTopFriend.Application.Campaigns.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Campaigns.Commands.Join;

public record JoinCampaignCommand(
    Guid Id,
    Guid CharacterId
) : IRequest<ErrorOr<CampaignJoinedResult>>;
