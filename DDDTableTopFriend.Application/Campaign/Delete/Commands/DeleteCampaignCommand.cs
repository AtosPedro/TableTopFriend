using DDDTableTopFriend.Application.Campaigns.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Delete.Commands;

public record DeleteCampaignCommand(
    Guid id
) : IRequest<ErrorOr<CampaignResult>>;
