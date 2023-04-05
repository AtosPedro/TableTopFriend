using DDDTableTopFriend.Application.Campaigns.Common;
using DDDTableTopFriend.Domain.Character.ValueObjects;
using DDDTableTopFriend.Domain.Session.ValueObjects;
using DDDTableTopFriend.Domain.Users.ValueObjects;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Create.Commands;

public record CreateCampaignCommand(
    Guid UserId,
    string Name,
    string Description,
    List<Guid> CharacterIds,
    List<Guid> SessionIds
) : IRequest<ErrorOr<CampaignResult>>;
