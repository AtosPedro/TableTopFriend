using DDDTableTopFriend.Application.Skills.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Skills.Queries.GetAll;

public record GetAllSkillsQuery(
    Guid UserId
) : IRequest<ErrorOr<IReadOnlyList<SkillResult>>>;
