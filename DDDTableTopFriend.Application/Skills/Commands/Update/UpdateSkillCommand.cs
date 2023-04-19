using DDDTableTopFriend.Application.Skills.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Skills.Commands.Update;

public record UpdateSkillCommand(
    Guid Id,
    Guid UserId,
    Guid StatusId,
    Guid AudioEffectId,
    string Name,
    string Description,
    float Cost
): IRequest<ErrorOr<SkillResult>>;
