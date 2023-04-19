using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Application.Skills.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Skills.Queries.GetAll;

public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, ErrorOr<IReadOnlyList<SkillResult>>>
{
    private readonly ISkillRepository _skillRepository;
    private readonly ICachingService _cachingService;

    public GetAllSkillsQueryHandler(
        ISkillRepository skillRepository,
        ICachingService cachingService)
    {
        _skillRepository = skillRepository;
        _cachingService = cachingService;
    }

    public Task<ErrorOr<IReadOnlyList<SkillResult>>> Handle(
        GetAllSkillsQuery request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
