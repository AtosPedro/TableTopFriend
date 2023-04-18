using DDDTableTopFriend.Application.Campaigns.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Update;

public record UpdateCampaignCommand(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    List<Guid> CharacterIds
) : IRequest<ErrorOr<CampaignResult>>;
