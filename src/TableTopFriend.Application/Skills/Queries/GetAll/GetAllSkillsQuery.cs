using TableTopFriend.Application.Skills.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Skills.Queries.GetAll;

public record GetAllSkillsQuery(
    Guid UserId
) : IRequest<ErrorOr<IReadOnlyList<SkillResult>>>;
