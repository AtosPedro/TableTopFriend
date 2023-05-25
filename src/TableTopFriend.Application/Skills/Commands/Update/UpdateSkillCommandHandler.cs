using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Application.Skills.Common;
using TableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using TableTopFriend.Domain.AggregateSkill.ValueObjects;
using TableTopFriend.Domain.AggregateStatus.ValueObjects;
using TableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace TableTopFriend.Application.Skills.Commands.Update;

public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand, ErrorOr<SkillResult>>
{
    private readonly ISkillRepository _skillRepository;
    private readonly IStatusRepository _statusRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICachingService _cachingService;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateSkillCommandHandler(
        ISkillRepository skillRepository,
        IDateTimeProvider dateTimeProvider,
        ICachingService cachingService,
        IStatusRepository statusRepository, IUnitOfWork unitOfWork)
    {
        _skillRepository = skillRepository;
        _dateTimeProvider = dateTimeProvider;
        _cachingService = cachingService;
        _statusRepository = statusRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<SkillResult>> Handle(
        UpdateSkillCommand request,
        CancellationToken cancellationToken)
    {
        var skill = await _skillRepository.GetById(
            SkillId.Create(request.Id),
            cancellationToken);

        if (skill is null)
            return Errors.Skill.NotRegistered;

        var status = await _statusRepository.GetById(
            StatusId.Create(request.StatusId),
            cancellationToken);

        if (status is null)
            return Errors.Status.NotRegistered;

        skill.Update(
            AudioEffectId.Create(request.AudioEffectId),
            StatusId.Create(request.StatusId),
            request.Name,
            request.Description,
            request.Cost,
            _dateTimeProvider.UtcNow
        );

        return await _unitOfWork.Execute(async _ =>
        {
            await _skillRepository.Update(skill);
            var result = skill.Adapt<SkillResult>();
            await _cachingService.SetCacheValueAsync(
                result.Id.ToString(),
                result);
            return result;
        },
        cancellationToken);
    }
}
