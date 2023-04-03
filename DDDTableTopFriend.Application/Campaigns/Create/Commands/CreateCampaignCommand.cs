using DDDTableTopFriend.Application.Campaigns.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Create.Commands;

public record CreateCampaignCommand(
    string Name,
    string Description
) : IRequest<ErrorOr<CampaignResult>>;
