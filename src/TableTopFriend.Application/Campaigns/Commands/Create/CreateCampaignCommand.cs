using TableTopFriend.Application.Campaigns.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Campaigns.Commands.Create;

public record CreateCampaignCommand(
    Guid UserId,
    string Name,
    string Description,
    List<Guid> CharacterIds
) : IRequest<ErrorOr<CampaignResult>>;
