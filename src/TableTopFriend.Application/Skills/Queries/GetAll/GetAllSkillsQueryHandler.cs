using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Application.Skills.Common;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using ErrorOr;
using Mapster;
using MediatR;

namespace TableTopFriend.Application.Skills.Queries.GetAll;

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

    public async Task<ErrorOr<IReadOnlyList<SkillResult>>> Handle(
        GetAllSkillsQuery request,
        CancellationToken cancellationToken)
    {
        var cachedSkills = await _cachingService.GetManyCacheValueAsync<SkillResult>(w => w.UserId == request.UserId);
        if (cachedSkills.Any())
            return cachedSkills;

        var skills = await _skillRepository.GetAll(
            UserId.Create(request.UserId),
            cancellationToken);

        var skillResults = new List<SkillResult>();
        foreach (var skill in skills)
        {
            var result = skill.Adapt<SkillResult>();
            skillResults.Add(result);
            await _cachingService.SetCacheValueAsync<SkillResult>(result.Id.ToString(), result);
        }

        return skillResults;
    }
}
