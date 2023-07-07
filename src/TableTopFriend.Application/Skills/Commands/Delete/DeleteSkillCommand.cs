using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Skills.Commands.Delete;

public record DeleteSkillCommand(
    Guid SkillId
): IRequest<ErrorOr<bool>>;
