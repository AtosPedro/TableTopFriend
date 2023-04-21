using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Application.Skills.Common;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
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

    public async Task<ErrorOr<SkillResult>> Handle(
        GetSkillQuery request,
        CancellationToken cancellationToken)
    {
        var skillCached = await _cachingService.GetCacheValueAsync<SkillResult>(request.SkillId.ToString());
        if (skillCached is not null)
            return skillCached;

        var skill = await _skillRepository.GetById(
            SkillId.Create(request.SkillId),
            cancellationToken);

        if (skill is null)
            return Errors.Skill.NotRegistered;

        var result = skill.Adapt<SkillResult>();
        await _cachingService.SetCacheValueAsync<SkillResult>(
            result.Id.ToString(),
            result);

        return result;
    }
}
