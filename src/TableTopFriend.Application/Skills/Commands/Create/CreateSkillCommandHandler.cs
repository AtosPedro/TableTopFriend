using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Application.Skills.Common;
using TableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using TableTopFriend.Domain.AggregateSkill;
using TableTopFriend.Domain.AggregateStatus.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Errors;
using TableTopFriend.Domain.Common.ValueObjects;
using ErrorOr;
using Mapster;
using MediatR;

namespace TableTopFriend.Application.Skills.Commands.Create;

public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, ErrorOr<SkillResult>>
{
    private readonly ISkillRepository _skillRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICachingService _cachingService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSkillCommandHandler(
        IDateTimeProvider dateTimeProvider,
        ICachingService cachingService,
        ISkillRepository skillRepository,
        IUnitOfWork unitOfWork)
    {
        _dateTimeProvider = dateTimeProvider;
        _cachingService = cachingService;
        _skillRepository = skillRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<SkillResult>> Handle(
        CreateSkillCommand request,
        CancellationToken cancellationToken)
    {
        var name = Name.Create(request.Name).Value;
        var skill = await _skillRepository.GetByName(
            UserId.Create(request.UserId),
            name,
            cancellationToken);

        if (skill is not null)
            return Errors.Skill.AlreadyRegistered;

        var skillOrError = Skill.Create(
            UserId.Create(request.UserId),
            AudioEffectId.Create(request.AudioEffectId),
            StatusId.Create(request.StatusId),
            request.Name,
            request.Description,
            request.Cost,
            _dateTimeProvider.UtcNow
        );

        if (skillOrError.IsError)
            return skillOrError.Errors;

        return await _unitOfWork.Execute(async cancellationToken =>
        {
            await _skillRepository.Add(skillOrError.Value, cancellationToken);
            var result = skillOrError.Value.Adapt<SkillResult>();
            await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
            return result;
        },
        cancellationToken);
    }
}
