using DDDTableTopFriend.Application.Campaigns.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Create.Commands;

public record UpdateCampaignCommand(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    List<Guid> CharacterIds,
    List<Guid> SessionIds
) : IRequest<ErrorOr<CampaignResult>>;
