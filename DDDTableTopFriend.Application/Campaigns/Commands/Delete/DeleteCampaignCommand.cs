using DDDTableTopFriend.Application.Campaigns.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Delete;

public record DeleteCampaignCommand(
    Guid Id
) : IRequest<ErrorOr<CampaignResult>>;
