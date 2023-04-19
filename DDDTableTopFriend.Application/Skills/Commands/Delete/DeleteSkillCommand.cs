using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Skills.Commands.Delete;

public record DeleteSkillCommand(
    Guid SkillId
): IRequest<ErrorOr<bool>>;
