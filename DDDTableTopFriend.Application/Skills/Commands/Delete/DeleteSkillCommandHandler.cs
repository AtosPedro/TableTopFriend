using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Application.Skills.Common;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Skills.Commands.Delete;

public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, ErrorOr<bool>>
{
    private readonly ISkillRepository _skillRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICachingService _cachingService;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteSkillCommandHandler(
        ISkillRepository skillRepository,
        IDateTimeProvider dateTimeProvider,
        ICachingService cachingService,
        IUnitOfWork unitOfWork)
    {
        _skillRepository = skillRepository;
        _dateTimeProvider = dateTimeProvider;
        _cachingService = cachingService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<bool>> Handle(
        DeleteSkillCommand request,
        CancellationToken cancellationToken)
    {
        var skill = await _skillRepository.GetById(SkillId.Create(request.SkillId), cancellationToken);
        if (skill is null)
            return Errors.Skill.NotRegistered;

        skill.MarkToDelete(_dateTimeProvider.UtcNow);

        return await _unitOfWork.Execute(async _ =>
        {
            await _skillRepository.Remove(skill);
            await _cachingService.RemoveCacheValueAsync<SkillResult>(skill.Id.Value.ToString());
            return skill is not null;
        },
        skill.DomainEvents,
        cancellationToken);
    }
}
