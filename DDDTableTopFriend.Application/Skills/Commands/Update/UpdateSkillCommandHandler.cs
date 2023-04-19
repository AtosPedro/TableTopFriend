using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Application.Skills.Common;
using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Skills.Commands.Update;

public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand, ErrorOr<SkillResult>>
{
    private readonly ISkillRepository _skillRepository;
    private readonly IStatusRepository _statusRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICachingService _cachingService;
    public UpdateSkillCommandHandler(
        ISkillRepository skillRepository,
        IDateTimeProvider dateTimeProvider,
        ICachingService cachingService,
        IStatusRepository statusRepository)
    {
        _skillRepository = skillRepository;
        _dateTimeProvider = dateTimeProvider;
        _cachingService = cachingService;
        _statusRepository = statusRepository;
    }
    public async Task<ErrorOr<SkillResult>> Handle(
        UpdateSkillCommand request,
        CancellationToken cancellationToken)
    {
        var skill = await _skillRepository.GetById(SkillId.Create(request.Id),cancellationToken);
        if (skill is null)
            return Errors.Skill.NotRegistered;

        var status = await _statusRepository.GetById(StatusId.Create(request.Id), cancellationToken);
        if (status is null)
            return Errors.Status.NotRegistered;

        skill.Update(
            AudioEffectId.Create(request.AudioEffectId),
            request.Name,
            request.Description,
            request.Cost,
            _dateTimeProvider.UtcNow
        );

        await _skillRepository.Update(skill);
        var result = skill.Adapt<SkillResult>();
        await _cachingService.SetCacheValueAsync(
            result.Id.ToString(),
            result);

        return result;
    }
}
