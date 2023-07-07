using TableTopFriend.Application.Campaigns.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Campaigns.Commands.Update;

public record UpdateCampaignCommand(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    List<Guid> CharacterIds
) : IRequest<ErrorOr<CampaignResult>>;
