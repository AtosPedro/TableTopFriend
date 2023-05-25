using TableTopFriend.Application.Campaigns.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Campaigns.Commands.Delete;

public record DeleteCampaignCommand(
    Guid Id
) : IRequest<ErrorOr<CampaignResult>>;
