using TableTopFriend.Application.Skills.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Skills.Commands.Create;

public record CreateSkillCommand(
    Guid UserId,
    Guid StatusId,
    Guid AudioEffectId,
    string Name,
    string Description,
    float Cost
) : IRequest<ErrorOr<SkillResult>>;
