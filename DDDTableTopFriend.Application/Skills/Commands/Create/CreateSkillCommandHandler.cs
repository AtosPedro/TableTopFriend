using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Application.Skills.Common;
using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSkill;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Skills.Commands.Create;

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
        var skill = await _skillRepository.GetByName(
            UserId.Create(request.UserId),
            request.Name,
            cancellationToken);

        if (skill is not null)
            return Errors.Skill.AlreadyRegistered;

        skill = Skill.Create(
            UserId.Create(request.UserId),
            AudioEffectId.Create(request.AudioEffectId),
            StatusId.Create(request.StatusId),
            request.Name,
            request.Description,
            request.Cost,
            _dateTimeProvider.UtcNow
        );

        return await _unitOfWork.Execute(async cancellationToken =>
        {
            await _skillRepository.Add(skill, cancellationToken);
            var result = skill.Adapt<SkillResult>();
            await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
            return result;
        },
        skill.DomainEvents,
        cancellationToken);
    }
}
