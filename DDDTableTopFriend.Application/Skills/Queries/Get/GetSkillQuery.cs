using DDDTableTopFriend.Application.Skills.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Skills.Queries.Get;

public record GetSkillQuery(
    Guid SkillId
) : IRequest<ErrorOr<SkillResult>>;
