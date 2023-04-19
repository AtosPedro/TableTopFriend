using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Application.Skills.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Skills.Queries.Get;

public class GetSkillQueryHandler : IRequestHandler<GetSkillQuery, ErrorOr<SkillResult>>
{
    private readonly ISkillRepository _skillRepository;
    private readonly ICachingService _cachingService;

    public GetSkillQueryHandler(
        ISkillRepository skillRepository,
        ICachingService cachingService)
    {
        _skillRepository = skillRepository;
        _cachingService = cachingService;
    }

    public Task<ErrorOr<SkillResult>> Handle(
        GetSkillQuery request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
