using TableTopFriend.Application.Skills.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Skills.Queries.Get;

public record GetSkillQuery(
    Guid SkillId
) : IRequest<ErrorOr<SkillResult>>;
