using DDDTableTopFriend.Application.Campaigns.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Create;

public record CreateCampaignCommand(
    Guid UserId,
    string Name,
    string Description,
    List<Guid> CharacterIds,
    List<Guid> SessionIds
) : IRequest<ErrorOr<CampaignResult>>;
